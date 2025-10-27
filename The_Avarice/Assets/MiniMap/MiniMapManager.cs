using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MiniMapManager : MonoBehaviour
{
    public static MiniMapManager Instance { get { return _instance; } }
    private static MiniMapManager _instance;

    public Camera miniMapCamera;
    public RawImage enlargeMapImage;
    private Vector2 mapSize;
    private PixelPerfectCamera ppc;

    private float zoomSpeed = 3f;
    private float oriZoom = 5f;

    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float currentZoom;

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
            CalculateMapSize();
            HandleMapZoom();
            HandleMapDrag();
        }
        else
        {
            miniMapCamera.orthographicSize = oriZoom;
            currentZoom = oriZoom;
            miniMapCamera.transform.position = Camera.main.transform.position;
        }
    }

    // �� ũ�� ���
    private void CalculateMapSize()
    {
        Rect rect = enlargeMapImage.rectTransform.rect;
        Vector2 size = new Vector2(rect.width, rect.height);
        Vector2 worldSize = size / enlargeMapImage.canvas.scaleFactor;
        mapSize = worldSize / 2f;
    }

    // �� ��
    private void HandleMapZoom()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            if (currentZoom < oriZoom)
            {
                currentZoom = oriZoom;
            }
            currentZoom -= scrollDelta * zoomSpeed;
            miniMapCamera.orthographicSize = currentZoom;
        }
    }

    // �� �巡��
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

    // ���� ī�޶� ���� �״�� ��������
    public void MCameraSetteings(Scene scene, LoadSceneMode mode)
    {
        miniMapCamera.gameObject.AddComponent<PixelPerfectCamera>();
        ppc = miniMapCamera.gameObject.GetComponent<PixelPerfectCamera>();

        ppc.assetsPPU = CameraManager.Instance.ppc.assetsPPU;
        ppc.refResolutionX = CameraManager.Instance.ppc.refResolutionX;
        ppc.refResolutionY = CameraManager.Instance.ppc.refResolutionY;
        ppc.cropFrame = CameraManager.Instance.ppc.cropFrame;
        ppc.gridSnapping = CameraManager.Instance.ppc.gridSnapping;
    }
}
