using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //일반공격
{
    public Animator animator;
    [SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f;

    private int comboStep = 0;
    private bool comboWindowOpen = false;
    private bool bufferedInput = false;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void input_Atk()
    {
        if (!isAttacking)
        {
            PlayAttack(1); // 첫 공격 시작
        }
        else if (comboWindowOpen && comboStep < 3)
        {
            PlayAttack(comboStep + 1); // 콤보 연결
        }
        else
        {
            bufferedInput = true; // 콤보 윈도우 밖 입력은 버퍼에 저장
            Debug.Log("Buffered input");
        }
    }

    void PlayAttack(int step)
    {
        comboStep = step;
        string triggerName = $"Attack{step}Trigger";
        animator.ResetTrigger("Attack1Trigger");
        animator.ResetTrigger("Attack2Trigger");
        animator.ResetTrigger("Attack3Trigger");
        animator.SetTrigger(triggerName);
        animator.speed = attackSpeed;

        comboWindowOpen = false;
        isAttacking = true;
        bufferedInput = false;

        Debug.Log($"Play Combo {step} (Speed: {attackSpeed})");
    }

    // 애니메이션 중간에 호출 (Animation Event)
    public void OpenComboWindow()
    {
        comboWindowOpen = true;
        Debug.Log("Combo Window Open");

        // 윈도우가 열린 순간, 입력 버퍼가 있다면 즉시 다음 공격
        if (bufferedInput && comboStep < 3)
        {
            PlayAttack(comboStep + 1);
        }
    }

    // 마지막 애니메이션 끝에 호출 (Animation Event)
    public void EndCombo()
    {
        Debug.Log("Combo End");
        comboStep = 0;
        comboWindowOpen = false;
        bufferedInput = false;
        isAttacking = false;
    }
}
