using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemWindowDoubleClick : MonoBehaviour,IPointerClickHandler
{
    private float interval = 0.3f;
    private float clickTime = 0f;
    private Item item;
    private void Start()
    {
        item=GetComponent<Item>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - clickTime) < interval)
        {
            if (item.itemenum == ItemEnum.NULL)//장비창에서 아이템을 가지고 있지 않다면
                return;

             foreach (var inventory in InventoryHandler.items)
            {
                if(inventory.itemenum ==ItemEnum.NULL)
                {
                    Debug.Log($"셋팅되는 :{inventory.name}");
                    inventory.itemenum= item.itemenum;
                    inventory.equipmentEnum = item.equipmentEnum;
                    inventory.itemImage.color= item.itemImage.color;

                    item.itemImage.color = new Color(255, 255, 255);
                    return;

                }
            }
           
            clickTime = 0f; // 초기화
        }
        else
        {
            clickTime = Time.time;
        }
    }
}
