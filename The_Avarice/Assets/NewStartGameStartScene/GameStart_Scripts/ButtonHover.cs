using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text hoverText;
     
    public void OnPointerEnter(PointerEventData eventData)
    {
      
        hoverText.fontSize++;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.fontSize--;
    }
}
