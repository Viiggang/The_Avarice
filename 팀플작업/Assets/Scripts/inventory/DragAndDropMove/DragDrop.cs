using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Inventory
{


    public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public RectTransform targetToMove; // �̵���ų ��� 
        private Vector2 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // �θ� ������ ���콺 ��ġ�� UI ��ġ ���� ���
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                targetToMove.parent as RectTransform,//�̵���ų ���
                eventData.position,//���콺 Ŭ�� ��ǥ
                eventData.pressEventCamera,//ī�޶�
                out Vector2 mousePosInParent// Ŭ����ǥ ��ȯ�� ���� ����
            );
            offset = mousePosInParent - (Vector2)targetToMove.localPosition;

        }

        public void OnDrag(PointerEventData eventData)
        {

            // ���� ���콺 ��ġ�� �θ� �������� ��������
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                targetToMove.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 mousePosInParent
            );

            targetToMove.localPosition = mousePosInParent - offset;
        }

    }
}

