using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDelete : MonoBehaviour,IEndDragHandler
{

    public void OnEndDrag(PointerEventData eventData)
    {
        var Item = eventData.pointerEnter.GetComponent<Item>(); //아이템 받아오기 <--임시 제네릭 인자
        if(Item == null)
        {
            Debug.Log("아이템 아님");
            return;
        }
        ///<summary>
        /// 이후 아이템의 값을 삭제
        /// 이미지 sprite null 처리
        /// 
        ///</summary>
       
        Item.itemenum = ItemEnum.NULL;
        Item.equipmentEnum = equipmentEnum.NULL;

    }
}
