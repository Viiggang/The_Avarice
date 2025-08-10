using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BossAnimactionEvents : MonoBehaviour
{
    [SerializeField]public Vector3 pos;
    [SerializeField] public Vector3 size;
    [SerializeField] public Transform BossPos;
    public LayerMask Player;
    
    bool hasCollider;
     
    public float knockbackForce = 5f;
    private IDamage PlayerHit;
    private float Damage=0;
    public bool flag = false;
    void OnDrawGizmos()
    {

        //// 구체 시각화 (중심 위치와 반지름)
        Gizmos.color = UnityEngine.Color.green;

        Gizmos.DrawWireCube(BossPos.transform.position + pos, size);
    }
    private void Start()
    {
        
    }

    private void SetPosAndSize(Vector3 pos , Vector3 size)
    {
        this.size = size;
        this.pos = pos;
    }
    public void NextToIdle()
    {
        NodeMachine.Instance.SetNextState("next");
    }
  
    public void SpinAttack()
    {
        SetPosAndSize(new Vector3(-0.05f, -0.14f, 0), new Vector3(0.69f, 0.28f, 0));//공격범위 설정

        var hit= Physics2D.OverlapBox(//플레이어 찾기
                BossPos.position + pos,//시작위치
                size,
                0f,
                Player);

        hasCollider = (hit != null) ? true : false;
        if (!hasCollider) return;//플레이어 찾았으면 진입 가능
        PlayerHit = hit.GetComponent<IDamage>();//플레이어 대미지 줄수있는 스크립트 가져오기
       if(PlayerHit ==null)//없으면 에러 처리
        {
            Debug.Log("SpinAttack-->PlayerHit 값이 없음");
        }

        PlayerHit.OnHitDamage(Damage);//대미지 주기



    }
    public void Blowing()
    {
        SetPosAndSize(new Vector3(-0.05f, 0.13f, 0), new Vector3(1.18f, 0.76f, 0));
     
        var hit = Physics2D.OverlapBox(
                       BossPos.position + pos,//시작위치
                       size,
                       0f,
                       Player);

        hasCollider = (hit != null) ? true : false;
        if (!hasCollider) return;
        Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        if (!rb) return;

        Vector2 knockbackDir = (hit.transform.position - BossPos.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

    }
    public void SlamDown()//meleeAttack
    {
        SetPosAndSize(new Vector3(-0.36f, -0.145f, 0), new Vector3(0.38f, -0.52f, 0));

        var hit = Physics2D.OverlapBox(
                       BossPos.position + pos,//시작위치
                       size,
                       0f,
                       Player);

        hasCollider = (hit !=null) ? true:false;
        if (!hasCollider) return;
        Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        if (!rb) return;

        Vector2 knockbackDir = (hit.transform.position - BossPos.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }
    public void purgeCannone()
    {
        SetPosAndSize(new Vector3(-0.3f, -0.16f, 0), new Vector3(0.33f, 0.57f, 0));
        var hit = Physics2D.OverlapBox(
                       BossPos.position + pos,//시작위치
                       size,
                       0f,
                       Player);

        hasCollider = hit is null;
        if (!hasCollider) return;
        Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        if (!rb) return;

        Vector2 knockbackDir = (hit.transform.position - pos).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }
    public void purgeShot()
    {
        

        
    }

  
  
  
    
}
