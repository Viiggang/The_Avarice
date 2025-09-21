using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get { return _instance; } }
    private static CameraManager _instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (_instance != null)
        {
            //Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        mainCamera.gameObject.SetActive(false);
        virtualCamera.gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        mainCamera.gameObject.SetActive(true);
        virtualCamera.gameObject.SetActive(true);

        virtualCamera.Follow = target; 
        virtualCamera.LookAt = target;
    }
}