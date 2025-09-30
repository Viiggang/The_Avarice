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

    private Vector2 defaultOffset = new Vector2(0f, 1f);
    private float fixedY;

    private bool wasJumpingLastFrame;

    private float lerpSpeed = 5f;

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
        if (virtualCamera.Follow == null) return;

        Animator animator = target.GetComponent<Animator>();
        bool isJump = animator.GetBool("isJump");

        Vector3 currentOffset = fT.m_TrackedObjectOffset;

        if (isJump)
        {
            if (!wasJumpingLastFrame)
            {
                fixedY = currentOffset.y;
            }

            fT.m_TrackedObjectOffset = new Vector3(defaultOffset.x, fixedY, currentOffset.z);
        }
        else
        {
            float newY = Mathf.Lerp(currentOffset.y, defaultOffset.y, Time.deltaTime * lerpSpeed);
            fT.m_TrackedObjectOffset = new Vector3(defaultOffset.x, newY, currentOffset.z);
        }

        wasJumpingLastFrame = isJump;
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