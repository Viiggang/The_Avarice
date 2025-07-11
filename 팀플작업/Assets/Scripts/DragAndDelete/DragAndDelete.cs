using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDelete : MonoBehaviour ,IBeginDragHandler, IDragHandler
{
    private RectTransform RectTransform;
    private Vector2 Pos=new Vector2();
    void VarGetComponent()
    {
        RectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        VarGetComponent();
        Pos = RectTransform.position;
        Debug.Log($"°ª:{Pos.x},{Pos.y}");
    }

 
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {

    }
}
