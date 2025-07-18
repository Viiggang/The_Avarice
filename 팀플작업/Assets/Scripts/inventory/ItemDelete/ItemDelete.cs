using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
namespace Inventory
{
    public class ItemDelete : MonoBehaviour, IDropHandler
    {

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("ItemDelete ����");
            var Item = eventData.pointerDrag.GetComponent<Item>(); //������ �޾ƿ��� 
            if (Item == null)
            {
                Debug.Log("ItemDelete ������ �ƴ�");
                return;
            }
            else
            {
                Debug.Log($"Item �̸�:{Item.name}");
                Debug.Log("OnPointerEnter �۵�");
            }
            ///<summary>
            /// ���� �������� ���� ����
            /// �̹��� sprite null ó��
            /// 
            ///</summary>

            Item.itemenum = ItemEnum.NULL;
            Item.equipmentEnum = equipmentEnum.NULL;
            Item.itemImage.color = Color.white;
        }
    }
}

