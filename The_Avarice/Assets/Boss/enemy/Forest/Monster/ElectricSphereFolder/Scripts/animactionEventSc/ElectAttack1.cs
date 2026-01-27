using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectAttack1", menuName = "Monster/AnimationEvents/ElectAttack1")]
public class ElectAttack1 :BaseAniEvent
{
    const float damage = 0.15f;
    public override void ElectricAttack( Vector3 Offset, Vector3 Size, LayerMask Player)//근접 공격
    {
    
        var Hit=Physics2D.OverlapBox(Offset,Size,0f,Player);
        if (Hit == null) return;

        //// 여기서 체력 받아오기 밑에 변수에 값 셋팅
        float MaxHp = PlayerMgr.instance.MaxHp;

        float finalDamage = (MaxHp * damage);
        var atk=Hit.GetComponent<IDamage>();
        atk?.OnHitDamage(-finalDamage);

    }
}
