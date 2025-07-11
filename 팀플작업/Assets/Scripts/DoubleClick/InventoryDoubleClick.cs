using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDoubleClick : MonoBehaviour,IPointerClickHandler
{
    private float interval = 0.3f; 
    private float clickTime = 0f;
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - clickTime) < interval)
        {

            useItem();
            clickTime = 0f; // 초기화
        }
        else
        {
            clickTime = Time.time;
        }
    }
    void useItem()
    {
        var item = this.gameObject.GetComponent<Item>();
        if (item == null)
        {
            Debug.Log("useItem 실패");
            return;
        }
        else
        {
            Debug.Log("useItem 성공");
            item.ItemUse();
        }
    }
}
