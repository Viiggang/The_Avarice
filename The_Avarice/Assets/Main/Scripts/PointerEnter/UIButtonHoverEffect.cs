using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverEffect : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    RectTransform m_RectTransform;//������Ʈ RectTransform
    Vector3 originalScale;//���� ������
    bool isHovered = false;
    private int originalSiblingIndex;
    Color m_Color;//���� ���� �÷��� ������ ����
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        originalScale = m_RectTransform.localScale;
        originalSiblingIndex = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovered) return;
        
         
            m_RectTransform.localScale = originalScale * 1.1f;
            isHovered = true;
            Debug.Log("Enter");
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHovered) return;
        m_RectTransform.localScale = originalScale;
        isHovered = false;
        Debug.Log("Exit");
        transform.SetSiblingIndex(originalSiblingIndex); // ���� ������ ����
    }
}
