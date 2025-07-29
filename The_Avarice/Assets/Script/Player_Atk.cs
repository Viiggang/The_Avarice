using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //일반공격
{
    private Animator animator;
    [SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f; // 공격속도
    [SerializeField]
    private int MaxComdo = 3; //최대 콤보수

    private int comboStep = 0; //현재 진행중인 콤보
    private bool comboWindowOpen = false; // 다음콤보입력
    private bool bufferedInput = false;// 입력버퍼
    private bool isAttacking = false; // 공격키 활성화 여부

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void input_Atk()
    {
        if (!isAttacking)
        {
            PlayAttack(1); // 첫 공격 시작;
        }
        else if (comboWindowOpen && comboStep < MaxComdo)
        {
            PlayAttack(comboStep + 1); // 콤보 연결
        }
        else if (comboStep == MaxComdo)
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
        string triggerName = $"Attack{step}Trigger";
        string ressettriggerName = $"Attack{step}Trigger";

        for (int i = 1; i < MaxComdo; i++)
        {
            ressettriggerName = $"Attack{i}Trigger";
            animator.ResetTrigger(triggerName);
        }
      
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

    public void EndCombo()
    {
        comboStep = 0;
        comboWindowOpen = false;
        bufferedInput = false;
        isAttacking = false;
    }
}
