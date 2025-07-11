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
        canvas = GetComponentInParent<Canvas>(); // Canvas �ʿ�
        image=GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = rectTransform.anchoredPosition;
        image.raycastTarget = false;

        // �巡�� ���� �� ���콺�� ������Ʈ�� �Ÿ� ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localMousePos);

        dragOffset = rectTransform.anchoredPosition - localMousePos;

        transform.parent.SetAsLastSibling(); // �� ������ ��������
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
