using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAniEvent : ScriptableObject
{
    public string trigger;
    public virtual void Execute(Rigidbody2D rigid2d,params object[] objects)
    {

    }
    public virtual void Execute(List<GameObject> bulletList, BulletPos bulletPos)//총알 발사
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

    public virtual void Execute(SpriteRenderer render, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//근접 공격
    {
        //render 좌 우 판별
        //콜라이더 좌표에서 offset 만큼 보정받은 후 공격
    }
    public virtual void Execute(SpriteRenderer render, Rigidbody2D rigid, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//돌진 공격
    {
        //render  flipx로 방향 체크 Dir 값 -1 1
        //rigid 방향으로 addfoce
        //콜라이더 좌표에서 offset 만큼 보정받은 후 공격
    }
    public virtual void Execute(ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//돌진 공격
    {
        //render  flipx로 방향 체크 Dir 값 -1 1
        //rigid 방향으로 addfoce
        //콜라이더 좌표에서 offset 만큼 보정받은 후 공격
    }
}


