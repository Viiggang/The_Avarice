using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AxemanAttack", menuName = "Monster/AnimationEvents/AxemanAttack")]
public class AxemanAttack : BaseAniEvent
{
    public override void Execute(SpriteRenderer render, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 size, LayerMask Player)//���� ����
    {
        //render �� �� �Ǻ�
        //�ݶ��̴� ��ǥ���� offset ��ŭ �������� �� ����
        directionCheck(render,ref Offset);
     
       
       

        var center= collider.bounds.center;
        var hit =Physics2D.OverlapBox(center+ Offset, size,0f, Player);
        hit.GetComponent<IDamage>().OnHitDamage(10f);
    }
    public void directionCheck(SpriteRenderer render ,ref Vector3 offet)
    {
        if (render.flipX)
        {
            offet.x = -0.24f;
            
        }
        else
        {
            offet.x = 0.24f;
        }
       
    }
}
