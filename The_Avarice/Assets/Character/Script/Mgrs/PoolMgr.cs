using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// PoolMgr: BaseMgr<T> 기반 싱글턴 풀 매니저
/// BaseMgr의 Awake()를 재정의하지 않고 그대로 사용합니다.
/// </summary>
[DisallowMultipleComponent]
public class PoolMgr : BaseMgr<PoolMgr>
{
    // 외부 호환성을 위해 Instance 프로퍼티 제공
    public static PoolMgr Instance => instance;

    [Serializable]
    public class PoolEntry
    {
        public string poolName;
        public GameObject prefab;
        [Tooltip("풀링된 오브젝트를 정리할 부모. 비워두면 자동 생성됨.")]
        public Transform parent;
        [Min(0)] public int defaultCapacity = 10;
        [Min(1)] public int maxSize = 50;
        [Tooltip("풀 생성 시 미리 만들어둘 오브젝트 수")]
        [Min(0)] public int warmupCount = 0;
        public bool collectionCheck = true;
    }

    [SerializeField] private List<PoolEntry> pools = new();

    private class PoolWrapper
    {
        public IObjectPool<GameObject> Pool;
        public Transform Parent;
        public GameObject Prefab;
        public int MaxSize;
    }

    private readonly Dictionary<string, PoolWrapper> poolMap = new(StringComparer.Ordinal);
    private const string PoolSuffix = "_Pool";

    #region Unity lifecycle

#if UNITY_EDITOR
    private void OnValidate()
    {
        var names = new HashSet<string>(StringComparer.Ordinal);
        for (int i = 0; i < pools.Count; i++)
        {
            var e = pools[i];
            if (e == null) continue;
            if (string.IsNullOrWhiteSpace(e.poolName))
            {
                e.poolName = $"Pool_{i}";
            }
            else
            {
                if (!names.Add(e.poolName))
                {
                    Debug.LogWarning($"[PoolMgr][OnValidate] 중복된 poolName 발견: '{e.poolName}' (index {i})");
                }
            }
            if (e.maxSize < 1) e.maxSize = 1;
            if (e.defaultCapacity < 0) e.defaultCapacity = 0;
            if (e.warmupCount < 0) e.warmupCount = 0;
        }
    }
#endif

    private void Start()
    {
        InitializePools();
    }

    #endregion

    #region Initialization

    private void InitializePools()
    {
        poolMap.Clear();

        foreach (var entry in pools)
        {
            if (entry == null) continue;

            string rawName = entry.poolName?.Trim();
            if (string.IsNullOrEmpty(rawName))
            {
                Debug.LogWarning("[PoolMgr] poolName이 비어있거나 null입니다. 스킵합니다.");
                continue;
            }
            if (entry.prefab == null)
            {
                Debug.LogWarning($"[PoolMgr] '{rawName}' 풀의 prefab이 할당되어있지 않습니다. 스킵합니다.");
                continue;
            }

            string key = Normalize(rawName);
            if (poolMap.ContainsKey(key))
            {
                Debug.LogWarning($"[PoolMgr] '{rawName}' 풀은 이미 존재합니다. 중복 항목 스킵.");
                continue;
            }

            Transform poolParent = entry.parent;
            if (poolParent == null)
            {
                var existing = GameObject.Find(rawName + PoolSuffix);
                poolParent = existing != null ? existing.transform : new GameObject(rawName + PoolSuffix).transform;
            }

            CreatePool(key, rawName, entry.prefab, poolParent, entry.defaultCapacity, entry.warmupCount, entry.maxSize, entry.collectionCheck);
        }
    }

    #endregion

    #region Pool creation & prewarm

    private void CreatePool(
     string key,
     string displayName,
     GameObject prefab,
     Transform poolParent,
     int defaultCapacity,
     int warmupCount,
     int maxSize,
     bool collectionCheck)
    {
        if (poolMap.ContainsKey(key)) return;

        var wrapper = new PoolWrapper
        {
            Parent = poolParent,
            Prefab = prefab,
            MaxSize = maxSize
        };

        ObjectPool<GameObject> pool = null;

        pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = Instantiate(prefab, poolParent);
                obj.SetActive(false);

                var meta = obj.GetComponent<PooledObject>();
                if (meta == null) meta = obj.AddComponent<PooledObject>();
                meta.poolName = displayName;
                meta.owner = pool;

                return obj;
            },
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
                if (obj.TryGetComponent<IPoolable>(out var poolable)) poolable.OnSpawn();
            },
            actionOnRelease: (obj) =>
            {
                if (obj.TryGetComponent<IPoolable>(out var poolable)) poolable.OnDespawn();
                obj.SetActive(false);
                obj.transform.SetParent(poolParent, false);
            },
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: collectionCheck && Debug.isDebugBuild,
            defaultCapacity: 0, // 0으로 두고 prewarm 사용
            maxSize: maxSize
        );

        wrapper.Pool = pool;
        poolMap[key] = wrapper;

        // ✅ warmupCount 적용
        int prewarm = Mathf.Max(defaultCapacity, warmupCount);
        for (int i = 0; i < prewarm; i++)
        {
            var o = pool.Get();      // 생성
            pool.Release(o);         // 비활성화 후 풀에 반환
        }
    }
    #endregion

    #region Public API

    public GameObject Get(string poolName, Vector3 position, Quaternion rotation, Transform parentForSpawn = null)
    {
        if (string.IsNullOrWhiteSpace(poolName))
        {
            Debug.LogWarning("[PoolMgr] Get 호출 시 poolName이 비어있습니다.");
            return null;
        }

        var key = Normalize(poolName);
        if (!poolMap.TryGetValue(key, out var wrapper) || wrapper?.Pool == null)
        {
            Debug.LogWarning($"[PoolMgr] '{poolName}' 풀을 찾을 수 없습니다.");
            return null;
        }

        var obj = wrapper.Pool.Get();
        obj.transform.SetParent(parentForSpawn, worldPositionStays: true);
        obj.transform.SetPositionAndRotation(position, rotation);

        return obj;
    }

    public void Release(GameObject obj)
    {
        if (obj == null) return;

        var meta = obj.GetComponent<PooledObject>();
        if (meta != null)
        {
            if (meta.owner != null)
            {
                meta.owner.Release(obj);
                return;
            }

            if (!string.IsNullOrWhiteSpace(meta.poolName))
            {
                var key = Normalize(meta.poolName);
                if (poolMap.TryGetValue(key, out var wrapper) && wrapper?.Pool != null)
                {
                    meta.owner = wrapper.Pool;
                    wrapper.Pool.Release(obj);
                    return;
                }
            }
        }

        Destroy(obj);
    }

    public void RegisterScenePooledObjects()
    {
        var all = FindObjectsOfType<PooledObject>(true);
        foreach (var p in all)
        {
            if (p == null) continue;
            if (string.IsNullOrWhiteSpace(p.poolName)) continue;

            var key = Normalize(p.poolName);
            if (poolMap.TryGetValue(key, out var wrapper) && wrapper?.Pool != null)
            {
                p.owner = wrapper.Pool;
                wrapper.Pool.Release(p.gameObject);
            }
            else
            {
                Debug.Log($"[PoolMgr] 씬에 배치된 '{p.poolName}' 오브젝트가 발견되었지만 풀은 등록되어있지 않습니다.");
            }
        }
    }

    public bool HasPool(string poolName) => poolMap.ContainsKey(Normalize(poolName));

    public void DestroyPool(string poolName, bool destroyInstances = true)
    {
        var key = Normalize(poolName);
        if (!poolMap.TryGetValue(key, out var wrapper)) return;

        if (destroyInstances && wrapper.Parent != null)
        {
            foreach (Transform child in wrapper.Parent)
            {
                Destroy(child.gameObject);
            }
        }
        poolMap.Remove(key);
    }

    public void DestroyAllPools(bool destroyInstances = true)
    {
        foreach (var kv in new List<KeyValuePair<string, PoolWrapper>>(poolMap))
        {
            DestroyPool(kv.Key, destroyInstances);
        }
    }

    #endregion

    #region Helpers
    private static string Normalize(string s) => s.Trim();
    #endregion
}

public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
}

public class PooledObject : MonoBehaviour
{
    [HideInInspector] public string poolName;
    [NonSerialized] public IObjectPool<GameObject> owner;
}
