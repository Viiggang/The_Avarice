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
            Debug.Log("ItemDelete 실행");
            var Item = eventData.pointerDrag.GetComponent<Item>(); //아이템 받아오기 
            if (Item == null)
            {
                Debug.Log("ItemDelete 아이템 아님");
                return;
            }
            else
            {
                Debug.Log($"Item 이름:{Item.name}");
                Debug.Log("OnPointerEnter 작동");
            }
            ///<summary>
            /// 이후 아이템의 값을 삭제
            /// 이미지 sprite null 처리
            /// 
            ///</summary>

            Item.itemenum = ItemEnum.NULL;
            Item.equipmentEnum = equipmentEnum.NULL;
            Item.itemImage.color = Color.white;
        }
    }
}

