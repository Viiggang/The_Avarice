using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDelete : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPos;
    private Vector2 dragOffset;
    Image image;
    void Start()
    {
       
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Canvas 필요
        image=GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = rectTransform.anchoredPosition;
        image.raycastTarget = false;

        // 드래그 시작 시 마우스와 오브젝트의 거리 저장
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localMousePos);

        dragOffset = rectTransform.anchoredPosition - localMousePos;

        transform.parent.SetAsLastSibling(); // 맨 앞으로 가져오기
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localMousePos))
        {
            rectTransform.anchoredPosition = localMousePos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPos;
        image.raycastTarget = true;
    }
}
