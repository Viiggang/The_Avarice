using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get { return _instance; } }
    private static CameraManager _instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public Collider2D cameraConfiner { get; set; }

    private CinemachineFramingTransposer fT;
    private CinemachineConfiner cF;

    private Transform target;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        fT = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        cF = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    private void LateUpdate()
    {
        fT.m_TrackedObjectOffset = new Vector3(0f, 2f, 0f);
    }

    public void SetTarget(Transform target)
    {
        virtualCamera.Follow = target; 
        this.target = target;
    }

    public void SetConfiner()
    {
        if (cameraConfiner != null)
        {
            cF.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
            cF.m_BoundingShape2D = cameraConfiner;
            cF.InvalidatePathCache();
        }
    }
}