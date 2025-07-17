using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Item : MonoBehaviour
{
    public ItemEnum itemenum;
    public equipmentEnum equipmentEnum;
    public Image itemImage;

    void Start()
    {
        init_item();
    }

    void init_item()
    {
        itemenum = ItemEnum.equipment;
        equipmentEnum = equipmentEnum.equipment_Weapon;
        itemImage= GetComponentsInChildren<Image>()
              .FirstOrDefault(img => img.gameObject != this.gameObject);
        itemImage.raycastTarget = false;
    }

    public void ItemUse()
    {
        Debug.Log("ItemUse ");
        if (itemenum == ItemEnum.NULL)//아이템이 없으면 
        {
            Debug.Log("아이템 없음");
            return;
        }
        
        if(itemenum == ItemEnum.equipment) //장비
        {
            Use_Item();
        }
        else //소비
        {
            Debug.Log("Consumption_Heal ");
        }
     

    }

    void Use_Item()
    {
        Debug.Log("ItemEnum.equipment ");
        foreach (var item in ItemWindowHandler.Instance.items)//아이템 창 순회
        {
            if (item.equipmentEnum == equipmentEnum)//무기 슬롯을 찾으면
            {
                if (ItemWindowHandler.Instance.WeaponFlag == false)//장비를 착용하고 있지 않다면
                {
                    SwapItemWithInventory1(item);
                    return;
                }
                else//장비를 착용 중이면 장비창 아이템을 빈곳에 옮기고 인벤토리에 장비창으로 옮김
                {
                    SwapItemWithInventory2(item);
                }

            }
        }

        void SwapItemWithInventory1(Item item)
        {
            itemenum = ItemEnum.NULL;// 현재 슬롯 타입 NULL
            equipmentEnum = equipmentEnum.NULL;//장비 타입 NULL
            item.itemImage.color = itemImage.color;//장비창에 현재 아이템 색을 입힌다.
            itemImage.color = Color.white;
            ItemWindowHandler.Instance.WeaponFlag = true;
            return;
        }

        void SwapItemWithInventory2(Item item)
        {
            foreach (var EmptySlot in InventoryHandler.items)//인벤토리 순회하면서 빈공간 찾기
            {

                if (EmptySlot.itemenum == ItemEnum.NULL)//슬롯에 아무것도 없음ㄴ
                {
                    //임시저장 변수
                    Item Temp = new Item();
                    GameObject itemObject = new GameObject("TempObject");
                    Temp.itemImage = itemObject.AddComponent<Image>(); // Image 컴포넌트 추가

                    // 슬롯 정보 얻기
                    Temp.itemenum = itemenum;
                    Temp.equipmentEnum = equipmentEnum;
                    Temp.itemImage.color = itemImage.color;

                    //현재슬롯 초기화
                    itemenum = ItemEnum.NULL;
                    equipmentEnum = equipmentEnum.NULL;
                    itemImage.color = Color.white;

                    //인벤토리에 빈슬롯에 장비창 아이템을 넘김
                    EmptySlot.itemenum = item.itemenum;
                    EmptySlot.equipmentEnum = item.equipmentEnum;
                    EmptySlot.itemImage.color = item.itemImage.color;

                    //장비창에 아이템 셋팅
                    item.itemenum = Temp.itemenum;
                    item.equipmentEnum = Temp.equipmentEnum;
                    item.itemImage.color = Temp.itemImage.color;

                    Destroy(itemObject);

                    return;
                }
            }
        }
    }
}
