using Colossal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpAttack", menuName = "Monster/AnimationEvents/JumpAttack")]
public class JumpAttack : BaseAniEvent
{
    public float dashForce; // µ¹Áø Èû
    public float jumpForce;//// Á¡ÇÁ Èû
   
    public override void Execute( MonsterController controller = null, params object[] data)
    {
        Rigidbody2D rigid  = new Rigidbody2D();
        AttackCollisionData Data = controller.MonsterAniEvent.attackCollisionData;
        MonsterAniEvents monsterAniEvents= controller.MonsterAniEvent;

        foreach (var item in data)
        {
            
            if(item is Rigidbody2D _rigid)
            {
                rigid=_rigid;
            }

        }
        const float left = -1;
        const float right = 1;
        float dirX = controller.Detection.renderer.flipX ? left : right;
        rigid.AddForce(new Vector2(dirX * dashForce, jumpForce),  ForceMode2D.Impulse);
       var hit = Physics2D.OverlapBox(controller.MonsterTrans.position, controller.statusManager.BoxCollider2D.bounds.size, 0f, Data.playerLayer);
        if (hit == null) return;
        var damage = hit.GetComponent<IDamage>();

        if (damage == null) return;
        damage.OnHitDamage(-10f);
    }
}
