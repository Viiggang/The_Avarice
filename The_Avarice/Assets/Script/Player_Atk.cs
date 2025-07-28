using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //�Ϲݰ���
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
            PlayAttack(1); // ù ���� ����
        }
        else if (comboWindowOpen && comboStep < 3)
        {
            PlayAttack(comboStep + 1); // �޺� ����
        }
        else
        {
            bufferedInput = true; // �޺� ������ �� �Է��� ���ۿ� ����
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

    // �ִϸ��̼� �߰��� ȣ�� (Animation Event)
    public void OpenComboWindow()
    {
        comboWindowOpen = true;
        Debug.Log("Combo Window Open");

        // �����찡 ���� ����, �Է� ���۰� �ִٸ� ��� ���� ����
        if (bufferedInput && comboStep < 3)
        {
            PlayAttack(comboStep + 1);
        }
    }

    // ������ �ִϸ��̼� ���� ȣ�� (Animation Event)
    public void EndCombo()
    {
        Debug.Log("Combo End");
        comboStep = 0;
        comboWindowOpen = false;
        bufferedInput = false;
        isAttacking = false;
    }
}
