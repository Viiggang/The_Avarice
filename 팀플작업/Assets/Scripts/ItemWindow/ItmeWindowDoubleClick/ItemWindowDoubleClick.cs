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
                Unequipped();//��� ����
                clickTime = 0f; // �ʱ�ȭ
            }
            else
            {
                clickTime = Time.time;
            }
        }

        private void Unequipped()
        {
            if (item.itemenum == ItemEnum.NULL)//���â���� �������� ������ ���� �ʴٸ�
                return;

            ItemWindowHandler.Instance.WeaponFlag = false;// ���� ��� ���������� �ʴٰ� �÷��� ����

            foreach (var inventory in InventoryHandler.items)//�κ��丮 List��ȸ
            {
                if (inventory.itemenum == ItemEnum.NULL)//�κ��丮 �� ���� ã����
                {
                    Debug.Log($"���õǴ� :{inventory.name}");
                    inventory.itemenum = item.itemenum; //���â�� ���->�κ��丮 itemenum ����
                    inventory.equipmentEnum = item.equipmentEnum; //���â�� ������ Ÿ�� ->�κ��丮 ������ Ÿ�� ����
                    inventory.itemImage.color = item.itemImage.color; //(�ӽ�) �������� �Ǵ� ���� ���� 

                    //���â�� ����itemenum,equipmentEnum �������־�� �°� ������ �� 

                    item.itemImage.color = new Color(255, 255, 255);//���� �������� ���
                    return;

                }
            }
        }
    }
}