using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public static MiniMapManager Instance { get { return _instance; } }
    private static MiniMapManager _instance;

    public Camera miniMapCamera;
    public RawImage enlargeMapImage;
    public CinemachineVirtualCamera virtualCamera;

    private PixelPerfectCamera ppc;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;    
    public float minLensSize = 10f;      
    public float maxLensSize = 60f;    

    private float targetLensSize;      

    private Vector3 dragOrigin;
    private bool isDragging = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += MCameraSetteings;
    }

    private void Start()
    {
        if (virtualCamera != null)
            targetLensSize = virtualCamera.m_Lens.OrthographicSize;
    }

    void LateUpdate()
    {
        if (gameObject.GetComponent<MiniMapController>().enlargeMap.gameObject.activeSelf)
        {
            HandleMapZoom();
            HandleMapDrag();
        }
        else
        {
            miniMapCamera.transform.position = Camera.main.transform.position;
        }
    }

    // 맵 줌
    private void HandleMapZoom()
    {
        var scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0)
        {
            zoomLevel += Mathf.RoundToInt(scrollWheelInput * 10);
            zoomLevel = Mathf.Clamp(zoomLevel, 1, 5);
            ppc.refResolutionX = Mathf.FloorToInt(Screen.width / zoomLevel);
            ppc.refResolutionY = Mathf.FloorToInt(Screen.height / zoomLevel);
        }
    }

    // 맵 드래그
    private void HandleMapDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = miniMapCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = dragOrigin - miniMapCamera.ScreenToWorldPoint(Input.mousePosition);
            miniMapCamera.transform.position += difference;
        }
    }

    // 메인 카메라 세팅 그대로 가져오기
    public void MCameraSetteings(Scene scene, LoadSceneMode mode)
    {
        if (miniMapCamera.gameObject.GetComponent<PixelPerfectCamera>() == null)
        {
            miniMapCamera.gameObject.AddComponent<PixelPerfectCamera>();
        }
        ppc = miniMapCamera.gameObject.GetComponent<PixelPerfectCamera>();

        ppc.assetsPPU = CameraManager.Instance.ppc.assetsPPU;
        ppc.refResolutionX = CameraManager.Instance.ppc.refResolutionX;
        ppc.refResolutionY = CameraManager.Instance.ppc.refResolutionY;
        ppc.cropFrame = CameraManager.Instance.ppc.cropFrame;
        ppc.gridSnapping = CameraManager.Instance.ppc.gridSnapping;
        targetLensSize = virtualCamera.m_Lens.OrthographicSize;
    }
}
