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
            clickTime = 0f; // �ʱ�ȭ
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
            Debug.Log("useItem ����");
            return;
        }
        else
        {
            Debug.Log("useItem ����");
            item.ItemUse();
        }
    }
}
