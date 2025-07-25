using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Atk : MonoBehaviour //�Ϲݰ���
{

    public GameObject[] AtkRange;// ȣ���� ���� ����

    Animator animator;

    [Header("-Attack Combo Count"),SerializeField]
    private int Atk_ComboCount;
    private int Combo_Count;// �޺����� ȣ��ī��Ʈ

    [SerializeField]
    private int Atk_ComboCount2;
    private int Combo_Count2;// �޺����� ȣ��ī��Ʈ

    [Space, Header("-Attack Charging Time"), SerializeField]
    private float Atk_ChargeCount;
    private float Charge_Count; //�����ð�

    [Space, Header("-Attack DelayTime"), SerializeField]
    private float Atk_Delay; // ���ݼӵ�
    [SerializeField]
    private float Atk_Delay2; // ���ݼӵ�

    private bool onAttack = true;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack() //���ݼ��� ��ũ��Ʈ
    {
        if (onAttack)
        {
            onAttack = false;
            ++Combo_Count;
            animator.SetBool("isAttack", true);
            animator.SetInteger("AtkCon", Combo_Count);

            if (Combo_Count > Atk_ComboCount)
            {
                ResetAttack();//�޺� �ʱ�ȭ
            }
        }
    }

    public void OnAirAttack() // ���� ���� ��ũ��Ʈ
    {
        if (onAttack)
        {
            onAttack = false;
            ++Combo_Count2;
            animator.SetBool("isAttack", true);
            animator.SetInteger("AtkCon", Combo_Count2);
            if (Combo_Count2 > Atk_ComboCount2)
            {
                ResetAttack();//�޺� �ʱ�ȭ
            }
        }
    }

    public void OnCharging() // �������� ��ũ��Ʈ
    {
        
    }

    private void ResetAttack()//�޺� �ʱ�ȭ
    {
        animator.SetBool("isAttack", false);
        Combo_Count = 0; 
        Combo_Count2 = 0;
        animator.SetInteger("AtkCon", Combo_Count);
        animator.SetInteger("AtkCon", Combo_Count2);
        onAttack = true;
    }

    public void AtkStart()
    {

    }

    public void AtkEnd()
    {

    }

    public void NextAtk()
    {
        onAttack = true;
    }

}
