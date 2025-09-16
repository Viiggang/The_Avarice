using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get { return _instance; } }
    private static CameraManager _instance;

    private CinemachineVirtualCamera virtualCamera;

    public GameObject mainCameraPrefab;
    public GameObject virtualCameraPrefab;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeCamera()
    {
        GameObject camera = Instantiate(mainCameraPrefab);
        DontDestroyOnLoad(camera);

        GameObject virtualcamera = Instantiate(virtualCameraPrefab);
        virtualcamera.transform.SetParent(camera.transform);
        DontDestroyOnLoad(virtualcamera);
        virtualCamera = virtualcamera.GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTarget(Transform target)
    {
        InitializeCamera();
        virtualCamera.Follow = target;
    }

}
