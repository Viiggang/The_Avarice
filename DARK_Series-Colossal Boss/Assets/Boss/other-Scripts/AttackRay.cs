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

        if(hit !=null)
        {
            Debug.Log($"{hit.collider.name}");
        }
      
      
    }
    public void BossDead()
    {
        Destroy(BossController.instance.gameObject);
    }
}
