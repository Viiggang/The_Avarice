using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class CameraConfinerRegister : MonoBehaviour
{
    public string addressableKey = "Confiner_";

    private GameObject currentConfinerInstance;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentConfinerInstance != null)
        {
            Destroy(currentConfinerInstance);
            currentConfinerInstance = null;
        }

        string addressKey = addressableKey + scene.name;

        StartCoroutine(AddressableKeyNullCheck(addressKey));
    }

    IEnumerator AddressableKeyNullCheck(string key)
    {
        var checkHandle = Addressables.LoadResourceLocationsAsync(key);
        yield return checkHandle;

        if (checkHandle.Status != AsyncOperationStatus.Succeeded || checkHandle.Result.Count == 0)
        {
            Addressables.Release(checkHandle); 
            yield break;
        }

        Addressables.LoadAssetAsync<GameObject>(key).Completed += OnConfinerPrefabLoaded;
    }

    private void OnConfinerPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning($"Failed to load : {handle.DebugName}");
            return;
        }

        GameObject confinerPrefab = handle.Result;

        currentConfinerInstance = Instantiate(confinerPrefab);
        currentConfinerInstance.name = "MapConfiner";

        var poly = currentConfinerInstance.GetComponent<PolygonCollider2D>();
        if (poly != null)
        {
            CameraManager.Instance.cameraConfiner = poly;
            CameraManager.Instance.SetConfiner();
        }

        Addressables.Release(handle);
    }
}