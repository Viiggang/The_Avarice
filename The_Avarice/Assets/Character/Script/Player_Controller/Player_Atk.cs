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

    [Header("Prefab Pool Settings")]
    public GameObject prefab;
    public int poolSize = 10;
    [Header("Offsets")]
    public Vector2 fireOffset = Vector2.zero;
    public Vector2 hitOffset = Vector2.zero;

    [SerializeField] 
  //  private float attackRange = 5f;
    private Rigidbody2D rb;
  

    private int comboStep = 0; //���� �������� �޺�
    private int currentHitIndex = 0;
    private bool comboWindowOpen = false; // �����޺��Է�
    private bool bufferedInput = false;// �Է¹���
    private bool isAttacking = false; // ����Ű Ȱ��ȭ ����

   // private int currentIndex = 0;






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




    void PlayAttack(int step)
    {
        comboStep = step;
        if (PlayerMgr.instance.playerType == Player_Type.Paladin && PlayerMgr.instance.OnPassive == true)
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
        animator.speed = PlayerMgr.instance.AttackSpeed;

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
        animator.speed = PlayerMgr.instance.Nomal_Speed;

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


    public void SetHitIndex(int index)
    {
        currentHitIndex = index;
    }

}
