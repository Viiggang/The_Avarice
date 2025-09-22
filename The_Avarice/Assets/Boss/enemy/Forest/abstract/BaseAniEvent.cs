using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAniEvent : ScriptableObject
{
    public string trigger;
    public virtual void Execute(Rigidbody2D rigid2d,params object[] objects)
    {

    }
    public virtual void Execute(List<GameObject> bulletList, BulletPos bulletPos)//�Ѿ� �߻�
    {

    }
    public virtual void Execute(MonsterController controller=null, params object[] data)
    {

    }
    public virtual void Execute(BossController controller, params object[] data)
    {

    }
    public virtual void Execute(ref Vector3 Offset, ref Vector3 Size,params object[] data)
    {

    }

    public virtual void Execute(SpriteRenderer render, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//���� ����
    {
        //render �� �� �Ǻ�
        //�ݶ��̴� ��ǥ���� offset ��ŭ �������� �� ����
    }
    public virtual void Execute(SpriteRenderer render, Rigidbody2D rigid, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//���� ����
    {
        //render  flipx�� ���� üũ Dir �� -1 1
        //rigid �������� addfoce
        //�ݶ��̴� ��ǥ���� offset ��ŭ �������� �� ����
    }
    public virtual void Execute(ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//���� ����
    {
        //render  flipx�� ���� üũ Dir �� -1 1
        //rigid �������� addfoce
        //�ݶ��̴� ��ǥ���� offset ��ŭ �������� �� ����
    }
}


