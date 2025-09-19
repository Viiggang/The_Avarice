using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[DisallowMultipleComponent]
public class PoolMgr : MonoBehaviour
{
    public static PoolMgr Instance { get; private set; }

    [Serializable]
    public class PoolEntry
    {
        public string poolName;
        public GameObject prefab;
        [Tooltip("풀링된 오브젝트를 정리할 부모. 비워두면 자동 생성됨.")]
        public Transform parent;
        [Tooltip("미리 생성할 배열의 크기.")]
        [Min(0)] public int defaultCapacity = 10;
        [Min(1)] public int maxSize = 50;
        [Tooltip("풀 생성 시 미리 만들어둘 오브젝트 수")]
        [Min(0)] public int warmupCount = 0;
        [Tooltip("중복 반환 방지 검사.")]
        public bool collectionCheck = true;
    }

    [SerializeField] private List<PoolEntry> pools = new();

    // PoolWrapper: 한 곳에서 풀 관련 데이터 관리
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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePools();
    }

    // 에디터에서 실수 방지용 (중복 이름 경고 등)
    private void OnValidate()
    {
#if UNITY_EDITOR
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
#endif
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

            //지정된 parent가 없으면 씬에서 찾거나 새로 만든다.
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
        // 이미 존재하면 리턴
        if (poolMap.ContainsKey(key)) return;

        // wrapper 준비
        var wrapper = new PoolWrapper
        {
            Parent = poolParent,
            Prefab = prefab,
            MaxSize = maxSize
        };

        // pool 변수는 먼저 null로 선언 -> 생성자에 defaultCapacity=0으로 만들고
        // 이후에 wrapper.Pool에 할당하고 수동으로 prewarm 해서 createFunc이 pool을 참조할 때 null이 아님을 보장
        ObjectPool<GameObject> pool = null;

        pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = Instantiate(prefab, poolParent);
                obj.SetActive(false);

                // PooledObject 컴포넌트 확보/세팅
                var meta = obj.GetComponent<PooledObject>();
                if (meta == null) meta = obj.AddComponent<PooledObject>();
                meta.poolName = displayName;
                meta.owner = pool; // 안전: createFunc은 pool이 할당된 이후(우리가 수동 prewarm/사용시) 호출됨

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
                obj.transform.SetParent(poolParent, worldPositionStays: false);
            },
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: collectionCheck && Debug.isDebugBuild,
            defaultCapacity: 0, // 0으로 두고 아래에서 수동 prewarm
            maxSize: maxSize
        );

        wrapper.Pool = pool;
        poolMap[key] = wrapper;

        // prewarm: defaultCapacity과 warmupCount 중 큰 값만큼 미리 생성(중복 생성 방지, 불필요 오브젝트 생성 최소화)
        int prewarm = Math.Max(defaultCapacity, warmupCount);
        for (int i = 0; i < prewarm; i++)
        {
            var o = pool.Get();
            pool.Release(o);
        }
    }

    #endregion

    #region Public API (Get / Release / Utility)

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

        // 부모 먼저 설정(세계 좌표 유지), 그 다음 위치/회전 세팅 — 안정적으로 동작하도록 순서를 맞춤
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

            // owner가 아직 비어있다면 poolName으로 찾아서 연결 후 Release
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

        // 풀에 속하지 않는 오브젝트이면 파괴
        Destroy(obj);
    }

    /// <summary>
    /// 씬에 미리 배치된 PooledObject를 찾아서 해당 풀에 등록(반환)합니다.
    /// includeInactive 오브젝트도 포함해서 검사합니다.
    /// </summary>
    public void RegisterScenePooledObjects()
    {
        var all = FindObjectsOfType<PooledObject>(true); // include inactive
        foreach (var p in all)
        {
            if (p == null) continue;
            if (string.IsNullOrWhiteSpace(p.poolName)) continue;

            var key = Normalize(p.poolName);
            if (poolMap.TryGetValue(key, out var wrapper) && wrapper?.Pool != null)
            {
                // owner 연결(씬에 배치된 인스턴스는 owner가 없을 수 있음)
                p.owner = wrapper.Pool;
                // pool에 넣음 (Release의 actionOnRelease가 비활성화/부모정리 처리)
                wrapper.Pool.Release(p.gameObject);
            }
            else
            {
                Debug.Log($"[PoolMgr] 씬에 배치된 '{p.poolName}' 오브젝트가 발견되었지만 풀은 등록되어있지 않습니다.");
            }
        }
    }

    /// <summary>풀 존재 여부</summary>
    public bool HasPool(string poolName) => poolMap.ContainsKey(Normalize(poolName));

    /// <summary>특정 풀을 제거(옵션: 소속 인스턴스 파괴)</summary>
    public void DestroyPool(string poolName, bool destroyInstances = true)
    {
        var key = Normalize(poolName);
        if (!poolMap.TryGetValue(key, out var wrapper)) return;

        if (destroyInstances)
        {
            // parent 아래 모든 자식 파괴
            if (wrapper.Parent != null)
            {
                var parentGO = wrapper.Parent.gameObject;
                foreach (Transform child in parentGO.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        poolMap.Remove(key);
    }

    /// <summary>모든 풀 제거 (주의: 인스턴스 파괴 포함)</summary>
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

/// <summary>풀링 오브젝트가 구현할 수 있는 인터페이스</summary>
public interface IPoolable
{
    void OnSpawn();   // Get 직후 호출
    void OnDespawn(); // Release 직전 호출
}

/// <summary>풀 소속 정보를 기록하는 메타데이터</summary>
public class PooledObject : MonoBehaviour
{
    [HideInInspector] public string poolName;
    [NonSerialized] public IObjectPool<GameObject> owner; // 런타임에만 사용
}
