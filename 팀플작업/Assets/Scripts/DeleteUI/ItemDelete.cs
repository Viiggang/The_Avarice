using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDelete : MonoBehaviour,IEndDragHandler
{

    public void OnEndDrag(PointerEventData eventData)
    {
        var Item = eventData.pointerEnter.GetComponent<Item>(); //������ �޾ƿ��� <--�ӽ� ���׸� ����
        if(Item == null)
        {
            Debug.Log("������ �ƴ�");
            return;
        }
        ///<summary>
        /// ���� �������� ���� ����
        /// �̹��� sprite null ó��
        /// 
        ///</summary>
       
        Item.itemenum = ItemEnum.NULL;
        Item.equipmentEnum = equipmentEnum.NULL;

    }
}
