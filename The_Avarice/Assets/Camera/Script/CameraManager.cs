using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CameraManager : OnScriptLoaded
{
    public static CameraManager Instance { get { return _instance; } }
    private static CameraManager _instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [HideInInspector] public PixelPerfectCamera ppc;
    public Collider2D cameraConfiner { get; set; }
    private Vector2Int tempResolution;

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

        tempResolution.x = Screen.width;
        tempResolution.y = Screen.height;

        Camera.main.gameObject.AddComponent<PixelPerfectCamera>();
        ppc = Camera.main.GetComponent<PixelPerfectCamera>() ?? null;

        if (ppc == null)
        {
            Debug.LogError("Pixel perfect camera doesn't exist");
            return;
        }

        ppc.gridSnapping = PixelPerfectCamera.GridSnapping.PixelSnapping;
        ppc.cropFrame = PixelPerfectCamera.CropFrame.Letterbox;

        UpdateResolution();

        SceneManager.sceneLoaded += SetPixelPerfectUnit;
    }

    private void Update()
    {
        if (Screen.width != tempResolution.x || Screen.height != tempResolution.y)
        {
            tempResolution.x = Screen.width;
            tempResolution.y = Screen.height;

            UpdateResolution();
            SetLensSize();
        }
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

    public void SetLensSize()
    {
        virtualCamera.m_Lens.OrthographicSize = Screen.height / (ppc.assetsPPU * 4) * 0.5f;
    }

    private void UpdateResolution()
    {
        if (ppc != null)
        {
            ppc.refResolutionX = Screen.width;
            ppc.refResolutionY = Screen.height;

            Debug.Log($"해상도 변경: {Screen.width} x {Screen.height}");
        }
        else
        {
            Debug.LogWarning("PixelPerfectCamera is not valid");
        }
    }

    public void SetPixelPerfectUnit(Scene scene, LoadSceneMode mode)
    {
        if (string.Compare(scene.name, "villageScene") == 0)
        {
            ppc.assetsPPU = 32;
            SetLensSize();
        }
        else if (string.Compare(scene.name, "forestScene") == 0)
        {
            ppc.assetsPPU = 16;
            SetLensSize();
        }

    }
}