using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum Attack_Type
{
    Close,
    Wide
};

public class Player_Atk : MonoBehaviour //�Ϲݰ���
{
    private Animator animator;
    [Header("- Attack Info"),SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f; // ���ݼӵ�
    private float nomal_Speed = 1.0f; // ���ݼӵ�
    [SerializeField]
    private int MaxComdo = 3; //�ִ� �޺���


    [SerializeField]
    Attack_Type atkType;
    [SerializeField]
    LayerMask hitMask;
    [SerializeField]
    private GameObject[] HitRange1;
    [SerializeField]
    private GameObject[] HitRange2;
    [SerializeField]
    private GameObject[] HitSkillRange1;
    [SerializeField]
    private GameObject[] HitSkillRange2;

    [SerializeField, Tooltip("0번은 기본생성위치 1번은 타겟위치 2번부터 투사체의 발사위치로 설정된다.")]
    private GameObject[] FirePoint;
    [SerializeField]
    private GameObject[] Bullet;

    [SerializeField] 
    private float attackRange = 5f;
    private Rigidbody2D rb;
  

    private int comboStep = 0; //���� �������� �޺�
    private int currentHitIndex = 0;
    private bool comboWindowOpen = false; // �����޺��Է�
    private bool bufferedInput = false;// �Է¹���
    private bool isAttacking = false; // ����Ű Ȱ��ȭ ����
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void input_Atk()
    {
        if (!isAttacking)
        {
            PlayAttack(0); // ù ���� ����;
        }
        else if (comboWindowOpen && comboStep < MaxComdo)
        {
            PlayAttack(comboStep + 1); // �޺� ����
        }
        else if (comboStep == MaxComdo - 1)
        {
            isAttacking = true;
        }
        else
        {
            bufferedInput = true; 
        }
    }

    public bool input_range()
    {
        //레이 캐스트를 이용해서 스킬이 생성될 위치를 구한다 방향은 플레이어가 보는 방향대로
        Vector2 origin = transform.position;
        Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, attackRange, hitMask);

        if (hit.collider != null)
        {
            // 충돌한 위치로 CaseHitPoint 이동
            FirePoint[1].transform.position = hit.point;
            Debug.DrawRay(origin, dir * hit.distance, Color.red, 0.3f);
            return true;
        }

        // 충돌하지 않았으면 아무 동작 없이 false 반환
        Debug.DrawRay(origin, dir * attackRange, Color.green, 0.3f);
        return false;
    }



    void PlayAttack(int step)
    {
        comboStep = step;
        if (PlayerMgr.instance.playerType == Player_Type.Paladin && PlayerMgr.instance.playerType == PlayerMgr.instance.playerType)
        {
            animator.SetTrigger("PassiveAtk");
        }
        else
        {
            for (int i = 0; i < MaxComdo; i++)
            {
                animator.ResetTrigger($"Attack{i}Trigger");
            }

            string triggerName = $"Attack{step}Trigger";
            animator.SetTrigger(triggerName);
        }
        animator.speed = attackSpeed;

        comboWindowOpen = false;
        isAttacking = true;
        bufferedInput = false;

    }

    // �ִϸ��̼� �̺�Ʈ
    public void OpenComboWindow()
    {
        comboWindowOpen = true;

        if (bufferedInput && comboStep < MaxComdo)
        {
            PlayAttack(comboStep + 1);
        }
    }

      public bool IsAttacking() => isAttacking;

    public void EndCombo()
    {
        comboStep = 0;
        comboWindowOpen = false;
        bufferedInput = false;
        isAttacking = false;
        animator.speed = nomal_Speed;

        // FSM���� �̵� �����ϵ��� ����
        var player = GetComponent<PlayerCon>();
        var stateMachine = GetComponent<Player_ControllMachine>();
        if (player != null)
            player.CanMove = true;
        if (stateMachine != null)
        stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 2f; // �⺻�� ����
    }

    public void OnAirAtk()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.gravityScale = 0f;
    }

    public void OutAirAtk()
    {
        rb.gravityScale = 3f;
        
    }


    public void OnHitRange(int type) // ���� ���� ȣ��� �ִϸ��̼� �̺�Ʈ
    {
        if (atkType == Attack_Type.Close)
        {
            if (type == 0 && currentHitIndex < HitRange1.Length)
            {
                HitRange1[currentHitIndex].SetActive(true);
            }
            else if (type == 1 && currentHitIndex < HitRange2.Length)
            {
                HitRange2[currentHitIndex].SetActive(true);
            }
            else if(type == 2 && currentHitIndex < HitSkillRange1.Length)
            {
                HitSkillRange1[currentHitIndex].SetActive(true);
            }
            else if (type == 3 && currentHitIndex < HitSkillRange2.Length)
            {
                HitSkillRange2[currentHitIndex].SetActive(true);
            }
          
        }
    }

    public void OnFire(int point, int type)
    {
        //투사체를 발사할 애니메이션 이벤트용 변수 point는 투사체가 발사될 위치의 배열, type은 발사될 투사체가 저장된 배열의 주소를 가리킨다.
    }
    public void SetHitIndex(int index)
    {
        currentHitIndex = index;
    }

}
