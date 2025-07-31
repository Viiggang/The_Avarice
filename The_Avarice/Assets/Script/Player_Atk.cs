using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //�Ϲݰ���
{
    private Animator animator;
    [Header("- Attack Info"),SerializeField, Range(0.5f, 2.5f)]
    private float attackSpeed = 1.0f; // ���ݼӵ�
    private float nomal_Speed = 1.0f; // ���ݼӵ�
    [SerializeField]
    private int MaxComdo = 3; //�ִ� �޺���

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


    private int comboStep = 0; //���� �������� �޺�
    private int currentHitIndex = 0;
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
        animator.speed = nomal_Speed;

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
        }
    }

    public void SetHitIndex(int index)
    {
        currentHitIndex = index;
    }

}
