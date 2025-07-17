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
        if (itemenum == ItemEnum.NULL)//�������� ������ 
        {
            Debug.Log("������ ����");
            return;
        }
        
        if(itemenum == ItemEnum.equipment) //���
        {
            Use_Item();
        }
        else //�Һ�
        {
            Debug.Log("Consumption_Heal ");
        }
     

    }

    void Use_Item()
    {
        Debug.Log("ItemEnum.equipment ");
        foreach (var item in ItemWindowHandler.Instance.items)//������ â ��ȸ
        {
            if (item.equipmentEnum == equipmentEnum)//���� ������ ã����
            {
                if (ItemWindowHandler.Instance.WeaponFlag == false)//��� �����ϰ� ���� �ʴٸ�
                {
                    SwapItemWithInventory1(item);
                    return;
                }
                else//��� ���� ���̸� ���â �������� ����� �ű�� �κ��丮�� ���â���� �ű�
                {
                    SwapItemWithInventory2(item);
                }

            }
        }

        void SwapItemWithInventory1(Item item)
        {
            itemenum = ItemEnum.NULL;// ���� ���� Ÿ�� NULL
            equipmentEnum = equipmentEnum.NULL;//��� Ÿ�� NULL
            item.itemImage.color = itemImage.color;//���â�� ���� ������ ���� ������.
            itemImage.color = Color.white;
            ItemWindowHandler.Instance.WeaponFlag = true;
            return;
        }

        void SwapItemWithInventory2(Item item)
        {
            foreach (var EmptySlot in InventoryHandler.items)//�κ��丮 ��ȸ�ϸ鼭 ����� ã��
            {

                if (EmptySlot.itemenum == ItemEnum.NULL)//���Կ� �ƹ��͵� ������
                {
                    //�ӽ����� ����
                    Item Temp = new Item();
                    GameObject itemObject = new GameObject("TempObject");
                    Temp.itemImage = itemObject.AddComponent<Image>(); // Image ������Ʈ �߰�

                    // ���� ���� ���
                    Temp.itemenum = itemenum;
                    Temp.equipmentEnum = equipmentEnum;
                    Temp.itemImage.color = itemImage.color;

                    //���罽�� �ʱ�ȭ
                    itemenum = ItemEnum.NULL;
                    equipmentEnum = equipmentEnum.NULL;
                    itemImage.color = Color.white;

                    //�κ��丮�� �󽽷Կ� ���â �������� �ѱ�
                    EmptySlot.itemenum = item.itemenum;
                    EmptySlot.equipmentEnum = item.equipmentEnum;
                    EmptySlot.itemImage.color = item.itemImage.color;

                    //���â�� ������ ����
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
