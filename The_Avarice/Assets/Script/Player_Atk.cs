using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //�Ϲݰ���
{
    private Animator animator;
    [SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f; // ���ݼӵ�
    [SerializeField]
    private int MaxComdo = 3; //�ִ� �޺���

    private int comboStep = 0; //���� �������� �޺�
    private bool comboWindowOpen = false; // �����޺��Է�
    private bool bufferedInput = false;// �Է¹���
    private bool isAttacking = false; // ����Ű Ȱ��ȭ ����

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void input_Atk()
    {
        if (!isAttacking)
        {
            PlayAttack(1); // ù ���� ����;
        }
        else if (comboWindowOpen && comboStep < MaxComdo)
        {
            PlayAttack(comboStep + 1); // �޺� ����
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

    // �ִϸ��̼� �̺�Ʈ
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
