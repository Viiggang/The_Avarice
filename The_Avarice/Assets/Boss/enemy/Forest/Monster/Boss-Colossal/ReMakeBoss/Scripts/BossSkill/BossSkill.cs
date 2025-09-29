using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossSkill
{
    [SerializeField] private string name;
    public BossAttackCollision CollisionData;//offset 이랑 size 데이터
   
    [System.Serializable]
    public class CoolTime
    {
        public  float Time;
        public bool Available=true;
    }
    public CoolTime coolTime;

    public string GetNodeName() =>  name;
    

    public bool GetAvailable()=> coolTime.Available;
    
    public float GetCoolTime()=> coolTime.Time;
    

    public Vector3 GetOffset()=> CollisionData.offset;
    
    public Vector3 GetOffset(SpriteRenderer flip)
    {
        Vector2 offset = CollisionData.offset;
        if (flip.flipX)
        {
            offset.x = -offset.x; // X축만 반전
        }
        return offset;
    }
    //public Vector3 GetOffset(SpriteRenderer flip, Collider2D box, Transform owner)
    //{
    //    Vector3 returnData = Vector3.zero;

    //        // 스케일 적용
    //        Vector3 scaledOffset = new Vector3(
    //            CollisionData.offset.x * Mathf.Abs(owner.localScale.x),
    //            CollisionData.offset.y * Mathf.Abs(owner.localScale.y),
    //            0f
    //        );

    //        // 방향(flipX)에 따라 X 방향 반전
    //        if (flip.flipX)
    //        {
    //            scaledOffset.x = -scaledOffset.x;
    //        }

    //        returnData = box.bounds.center + scaledOffset;

    //    return returnData;
    //}
    public Vector3 GetOffset(SpriteRenderer flip, Transform owner)
    {
        // 1. 원본 오프셋
        Vector3 offset = CollisionData.offset;

        // 2. 스케일 적용 (절대값으로 확장)
        offset.x = offset.x *Mathf.Abs(owner.lossyScale.x);
        offset.y = offset.y *Mathf.Abs(owner.lossyScale.y);

        // 3. flipX에 따라 좌우 반전
        if (flip != null && flip.flipX)
        {
            offset.x = -offset.x;
        }

        // 4. 최종 위치 계산: 기준점(owner 위치) + 오프셋
        return owner.position + offset;
    }
    public Vector3 GetSize()=> CollisionData.Size;
   

    public BossAttackCollision GetData()=> CollisionData;
   

    public void StartCoolTime(MonoBehaviour owner)
    {
        if (coolTime.Time > 0)
        {
            owner.StartCoroutine(OnCoolTime());
        }
    }
    //private void OnCoolTime( )
    //{
    //    coolTime.Available = true;
    //    Debug.Log("쿨타임 끝");
    //}
    public IEnumerator OnCoolTime()
    {
        this.coolTime.Available = false;
        yield return new WaitForSeconds(this.coolTime.Time);
        coolTime.Available = true;

    }

}
