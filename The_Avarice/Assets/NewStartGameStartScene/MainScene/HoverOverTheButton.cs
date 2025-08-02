using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOverTheButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite tempSprite;

    [SerializeField] private Text scaleText;
    [SerializeField] private Image scaleImage;
    [SerializeField] private Sprite changedSprite;
    void Awake()
    {
        tempSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleImage) scaleImage.transform.localScale += new Vector3(0.05f, 0.05f, 0f);
        if (scaleText) scaleText.fontSize++;
        if (changedSprite) gameObject.GetComponent<Image>().sprite = changedSprite;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleImage) scaleImage.transform.localScale -= new Vector3(0.05f, 0.05f, 0f);
        if (scaleText) scaleText.fontSize--;
        if (changedSprite) gameObject.GetComponent<Image>().sprite = tempSprite;
    }
}
