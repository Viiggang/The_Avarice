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
        if (itemenum == ItemEnum.NULL)//�������� ������ 
        {
            Debug.Log("������ ����");
            return;
        }
        
        if(itemenum == ItemEnum.equipment) //���
        {
            Debug.Log("ItemEnum.equipment ");
            foreach(var item in ItemWindowHandler.Instance.items)//������ â ��ȸ
            {
                 if(item.equipmentEnum == equipmentEnum)//���� ������ ã����
                 {
                    if(ItemWindowHandler.Instance.WeaponFlag == false)//��� �����ϰ� ���� �ʴٸ�
                    {
                        itemenum = ItemEnum.NULL;// ���� ���� Ÿ�� NULL
                        equipmentEnum = equipmentEnum.NULL;//��� Ÿ�� NULL
                        item.itemImage.color = itemImage.color;//���â�� ���� ������ ���� ������.
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
                                //���â���� �κ��丮��
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
        else //�Һ�
        {
            Debug.Log("Consumption_Heal ");
        }
     

    }

    void Start()
    {
        init_item();
    }

}
