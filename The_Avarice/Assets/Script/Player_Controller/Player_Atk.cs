using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //일반공격
{
    private Animator animator;
    [Header("- Attack Info"),SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f; // 공격속도
    private float nomal_Speed = 1.0f; // 공격속도
    [SerializeField]
    private int MaxComdo = 3; //최대 콤보수

    enum Attack_Type
    {
        Close,
        Wide
    };
    [SerializeField]
    Attack_Type atkType;

    [SerializeField]
    private GameObject[] HitRange1;
    [SerializeField]
    private GameObject[] HitRange2;
    [SerializeField]
    private GameObject[] HitSkillRange1;
    [SerializeField]
    private GameObject[] HitSkillRange2;

    private Rigidbody2D rb;

    private int comboStep = 0; //현재 진행중인 콤보
    private int currentHitIndex = 0;
    private bool comboWindowOpen = false; // 다음콤보입력
    private bool bufferedInput = false;// 입력버퍼
    private bool isAttacking = false; // 공격키 활성화 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void input_Atk()
    {
        if (!isAttacking)
        {
            PlayAttack(0); // 첫 공격 시작;
        }
        else if (comboWindowOpen && comboStep < MaxComdo)
        {
            PlayAttack(comboStep + 1); // 콤보 연결
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

    void PlayAttack(int step)
    {
        comboStep = step;

        for (int i = 0; i < MaxComdo; i++)
        {
            animator.ResetTrigger($"Attack{i}Trigger");
        }

        string triggerName = $"Attack{step}Trigger";
        animator.SetTrigger(triggerName);

        animator.speed = attackSpeed;

        comboWindowOpen = false;
        isAttacking = true;
        bufferedInput = false;

    }

    // 애니메이션 이벤트
    public void OpenComboWindow()
    {
        comboWindowOpen = true;

        if (bufferedInput && comboStep < 3)
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

        // FSM에서 이동 가능하도록 복구
        var player = GetComponent<PlayerCon>();
        var stateMachine = GetComponent<Player_ControllMachine>();
        if (player != null)
            player.CanMove = true;
        if (stateMachine != null)
        stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 2f; // 기본값 복원
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


    public void OnHitRange(int type) // 공격 범위 호출용 애니메이션 이벤트
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
            else if(type == 2 && currentHitIndex < HitRange2.Length)
            {
                HitSkillRange1[currentHitIndex].SetActive(true);
            }
        }
    }

    public void SetHitIndex(int index)
    {
        currentHitIndex = index;
    }

}
