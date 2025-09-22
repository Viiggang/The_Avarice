using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossSkill 
{
    public BossAttackCollision CollisionData;//offset �̶� size ������
    [SerializeField]private string name;
    [System.Serializable]
    public class CoolTime
    {
        public  float Time;
        public bool Available=true;
    }
    [SerializeField] public CoolTime coolTime;

    public string GetNodeName()
    {
        return name;
    }


    public bool GetAvailable()
    {
 
        return coolTime.Available;
    }
    public float GetCoolTime()
    {
        return coolTime.Time;
    }

    public Vector3 GetOffset()
    {
        return CollisionData.offset;
    }
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
    public Vector3 GetSize()
    {
        return CollisionData.Size;
    }

    public BossAttackCollision GetData()
    {
        return CollisionData;
    }

    public void StartCoolTime(MonoBehaviour owner)
    {
        if (coolTime.Time > 0)
        {
            this.coolTime.Available = false;
            owner.Invoke("OnCoolTime", this.coolTime.Time);
        }
    }
    private void OnCoolTime( )
    {
        coolTime.Available = true;
        Debug.Log("��Ÿ�� ��");
    }

}
