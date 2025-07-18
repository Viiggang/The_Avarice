using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Inventory
{


    public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public RectTransform targetToMove; // 이동시킬 대상 
        private Vector2 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 부모 기준의 마우스 위치와 UI 위치 차이 계산
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                targetToMove.parent as RectTransform,//이동시킬 대상
                eventData.position,//마우스 클릭 좌표
                eventData.pressEventCamera,//카메라
                out Vector2 mousePosInParent// 클릭좌표 반환값 저장 변수
            );
            offset = mousePosInParent - (Vector2)targetToMove.localPosition;

        }

        public void OnDrag(PointerEventData eventData)
        {

            // 현재 마우스 위치를 부모 기준으로 가져오기
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

