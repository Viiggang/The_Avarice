using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossSkill
{
    [SerializeField] private string name;
    public BossAttackCollision CollisionData;//offset �̶� size ������
   
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
            offset.x = -offset.x; // X�ุ ����
        }
        return offset;
    }
    //public Vector3 GetOffset(SpriteRenderer flip, Collider2D box, Transform owner)
    //{
    //    Vector3 returnData = Vector3.zero;

    //        // ������ ����
    //        Vector3 scaledOffset = new Vector3(
    //            CollisionData.offset.x * Mathf.Abs(owner.localScale.x),
    //            CollisionData.offset.y * Mathf.Abs(owner.localScale.y),
    //            0f
    //        );

    //        // ����(flipX)�� ���� X ���� ����
    //        if (flip.flipX)
    //        {
    //            scaledOffset.x = -scaledOffset.x;
    //        }

    //        returnData = box.bounds.center + scaledOffset;

    //    return returnData;
    //}
    public Vector3 GetOffset(SpriteRenderer flip, Transform owner)
    {
        // 1. ���� ������
        Vector3 offset = CollisionData.offset;

        // 2. ������ ���� (���밪���� Ȯ��)
        offset.x = offset.x *Mathf.Abs(owner.lossyScale.x);
        offset.y = offset.y *Mathf.Abs(owner.lossyScale.y);

        // 3. flipX�� ���� �¿� ����
        if (flip != null && flip.flipX)
        {
            offset.x = -offset.x;
        }

        // 4. ���� ��ġ ���: ������(owner ��ġ) + ������
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
    //    Debug.Log("��Ÿ�� ��");
    //}
    public IEnumerator OnCoolTime()
    {
        this.coolTime.Available = false;
        yield return new WaitForSeconds(this.coolTime.Time);
        coolTime.Available = true;

    }

}
