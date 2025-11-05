using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public static MiniMapManager Instance { get { return _instance; } }
    private static MiniMapManager _instance;

    public Camera miniMapCamera;
    public RawImage enlargeMapImage;

    private PixelPerfectCamera ppc;

    private float zoomLevel = 1f;
    private float minZoom = 1f;
    private float maxZoomX = 5f;
    private float maxZoomY = 5f;

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


    void LateUpdate()
    {
        if (gameObject.GetComponent<MiniMapController>().enlargeMap.gameObject.activeSelf)
        {


            UpdateZoomRangeByMapSize();
            HandleMapZoom();
            HandleMapDrag();
        }
        else
        {
            miniMapCamera.transform.position = Camera.main.transform.position;
        }
    }
    private void UpdateZoomRangeByMapSize()
    {
        if (enlargeMapImage == null) return;

        RectTransform rt = enlargeMapImage.rectTransform;
        float mapWidth = rt.rect.width;
        float mapHeight = rt.rect.height;

        // 화면 대비 비율 계산
        float widthRatio = mapWidth / Screen.width;
        float heightRatio = mapHeight / Screen.height;

        // X/Y 각각 최대 줌 레벨 계산 (비율이 클수록 줌 아웃 가능)
        maxZoomX = Mathf.Clamp(widthRatio * 4f, 2f, 10f);
        maxZoomY = Mathf.Clamp(heightRatio * 4f, 2f, 10f);

        minZoom = 1f;
    }

    // 맵 줌
    private void HandleMapZoom()
    {
        var scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0)
        {
            zoomLevel += scrollWheelInput * 5f;
            zoomLevel = Mathf.Clamp(zoomLevel, minZoom, Mathf.Min(maxZoomX, maxZoomY));

            if (ppc != null)
            {
                // X/Y축을 각자 비율에 맞게 나누되, 화면 비율 유지
                float zoomFactorX = Mathf.Lerp(1f, 1f / maxZoomX, (zoomLevel - minZoom) / (maxZoomX - minZoom));
                float zoomFactorY = Mathf.Lerp(1f, 1f / maxZoomY, (zoomLevel - minZoom) / (maxZoomY - minZoom));

                ppc.refResolutionX = Mathf.FloorToInt(Screen.width * zoomFactorX);
                ppc.refResolutionY = Mathf.FloorToInt(Screen.height * zoomFactorY);
            }
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
    }
}
