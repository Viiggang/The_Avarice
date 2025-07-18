using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using   Inventory;
namespace ItemWindow
{
    public class ItemWindowDoubleClick : MonoBehaviour, IPointerClickHandler
    {
        private float interval = 0.3f;
        private float clickTime = 0f;
        private Item item;
        private void Start()
        {
            item = GetComponent<Item>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if ((Time.time - clickTime) < interval)
            {
                Unequipped();//장비 해제
                clickTime = 0f; // 초기화
            }
            else
            {
                clickTime = Time.time;
            }
        }

        private void Unequipped()
        {
            if (item.itemenum == ItemEnum.NULL)//장비창에서 아이템을 가지고 있지 않다면
                return;

            ItemWindowHandler.Instance.WeaponFlag = false;// 현재 장비를 착용중이지 않다고 플래그 설정

            foreach (var inventory in InventoryHandler.items)//인벤토리 List순회
            {
                if (inventory.itemenum == ItemEnum.NULL)//인벤토리 빈 곳을 찾으면
                {
                    Debug.Log($"셋팅되는 :{inventory.name}");
                    inventory.itemenum = item.itemenum; //장비창의 장비->인벤토리 itemenum 설정
                    inventory.equipmentEnum = item.equipmentEnum; //장비창의 아이템 타입 ->인벤토리 아이템 타입 설정
                    inventory.itemImage.color = item.itemImage.color; //(임시) 아이템이 되는 색상 셋팅 

                    //장비창의 슬롯itemenum,equipmentEnum 고정되있어야 맞게 장착이 됨 

                    item.itemImage.color = new Color(255, 255, 255);//현재 아이템을 비움
                    return;

                }
            }
        }
    }
}