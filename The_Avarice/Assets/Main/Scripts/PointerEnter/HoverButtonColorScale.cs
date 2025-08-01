using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;
public class HoverButtonColorScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform m_RectTransform;//������Ʈ RectTransform
    Vector3 originalScale;//���� ������
    bool isHovered = false;
    Text m_Text;//�ڽ� �ؽ�Ʈ ������Ʈ ������ ���� 
    Color m_Color;//���� ���� �÷��� ������ ����
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        originalScale = m_RectTransform.localScale;
        m_Text=GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovered) return;
        m_Color = m_Text.color;
        m_Text.color = Color.red;
        string layerName = LayerMask.LayerToName(gameObject.layer);
        if (layerName.StartsWith("UI_"))
        {
            m_RectTransform.localScale = originalScale * 1.1f;
            isHovered = true;
            Debug.Log("Enter");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHovered) return;
        m_Text.color = m_Color;
        m_RectTransform.localScale = originalScale;
        isHovered = false;
        Debug.Log("Exit");
    }
}
