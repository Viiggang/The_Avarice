using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ElectAttack2", menuName = "Monster/AnimationEvents/ElectAttack2")]
public class ElectAttack2 : BaseAniEvent
{
    const float damage = 0.3f;

    public override void Execute(SpriteRenderer render, BoxCollider2D collider, ref Vector3 Offset, ref Vector3 Size, LayerMask Player)//���� ����
    {
        var center = collider.bounds.center;
        var Hit = Physics2D.OverlapBox(center + Offset, Size, 0f, Player);
        if (Hit == null) return;

        // ���⼭ ü�� �޾ƿ��� �ؿ� ������ �� ����
        float MaxHp = PlayerMgr.instance.getPlayerMaxHp();


        float finalDamage= (MaxHp* damage);
        Hit.GetComponent<IDamage>().OnHitDamage(-finalDamage);
    }
}
