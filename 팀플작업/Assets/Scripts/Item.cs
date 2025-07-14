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
            Debug.Log("ItemEnum.equipment ");
            foreach(var item in ItemWindowHandler.Instance.items)//아이템 창 순회
            {
                 if(item.equipmentEnum == equipmentEnum)//무기 슬롯을 찾으면
                 {
                    if(ItemWindowHandler.Instance.WeaponFlag == false)//장비를 착용하고 있지 않다면
                    {
                        itemenum = ItemEnum.NULL;// 현재 슬롯 타입 NULL
                        equipmentEnum = equipmentEnum.NULL;//장비 타입 NULL
                        item.itemImage.color = itemImage.color;//장비창에 현재 아이템 색을 입힌다.
                        itemImage.color = new Color(255, 255, 255);
                        ItemWindowHandler.Instance.WeaponFlag = true;
                        return;
                    }
                    else
                    {

                         foreach(var item1 in InventoryHandler.items)
                         {
                            //w
                            if(item1.itemenum ==ItemEnum.NULL)
                            {
                                ItemEnum A;
                                equipmentEnum B;
                                Color C;

                                A = item.itemenum;
                                B = item.equipmentEnum;
                                C = item.itemImage.color;
                                //장비창에서 인벤토리로
                                item1.itemenum = A;
                                item1.equipmentEnum = B;
                                item1.itemImage.color= C;

                                item.itemenum = itemenum;
                                item.equipmentEnum = equipmentEnum;
                                item.itemImage.color = itemImage.color;

                                itemenum = ItemEnum.NULL;
                                equipmentEnum = equipmentEnum.NULL;
                                itemImage.color = new Color(255, 255, 255);
                                
                                return;
                            }
                         }
                    }
                  
                 }
            }
            

        }
        else //소비
        {
            Debug.Log("Consumption_Heal ");
        }
     

    }

    void Start()
    {
        init_item();
    }

}
