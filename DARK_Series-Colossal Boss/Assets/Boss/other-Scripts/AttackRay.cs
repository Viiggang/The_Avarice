using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AttackRay : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 size;
    public float distance = 1f;
    public LayerMask Player;

    bool hasCollider;
    public void SuperAttack()
    {
        bool flipx = BossController.instance.spriteRenderer.flipX;
        pos = flipx? new Vector3(-3.05f, -1.04f, 0f): new Vector3(2.95f, -1.04f, 0f);
        
       
        size = new Vector3(6.02f, 2.04f, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(
        BossController.instance.transform.position + pos,      // 시작 위치
        size,//크기
        0f,//각도
         BossController.instance.transform.right,//방향
        distance,//거리
        Player);//레이어 마스크
        hasCollider = hit.collider == null ? false : true;
        if (hasCollider)
        {
            Debug.Log($"{hit.collider.name}");
        }

        idlestate();
    }
    public void RangeAttack()
    {
        bool flipx = BossController.instance.spriteRenderer.flipX;
        pos = flipx?   new Vector2(-3.43f, -0.97f): new Vector2(3.85f, -0.97f); //true(오른쪽)
        size = new Vector2(3.68f, 1.98f);
        RaycastHit2D hit = Physics2D.BoxCast(
        BossController.instance.transform.position + pos,      // 시작 위치
         size,//크기
        0f,//각도
        BossController.instance.transform.right,//방향
        distance,//거리
        Player);//레이어 마스크

        hasCollider = hit.collider == null ? false : true;
        if (hasCollider)
        {
            Debug.Log($"{hit.collider.name}");
        }
        idlestate();
    }
    public void MeleeAttack()
    {
        pos = new Vector2(-0.17f, -0.97f);
        size = new Vector2(6.74f, 1.98f);
      RaycastHit2D hit = Physics2D.BoxCast(
      BossController.instance.transform.position + pos,      // 시작 위치
      size,//크기
      0f,//각도
       BossController.instance.transform.right,//방향
      distance,//거리
      Player);//레이어 마스크

        hasCollider= hit.collider == null ? false : true;
        if (hasCollider)
        {
            Debug.Log($"{hit.collider.name}");
        }
        idlestate();
    }
    public void BossDead()
    {
        Destroy(BossController.instance.gameObject);
    }
    private void idlestate()
    {
        BossController.instance.ChangeState(new IdleState());
    }
}
