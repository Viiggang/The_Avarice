using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Atk : MonoBehaviour //일반공격
{

    public GameObject[] AtkRange;// 호출할 공격 범위

    Animator animator;

    [Header("-Attack Combo Count"),SerializeField]
    private int Atk_ComboCount;
    private int Combo_Count;// 콤보공격 호출카운트

    [SerializeField]
    private int Atk_ComboCount2;
    private int Combo_Count2;// 콤보공격 호출카운트

    [Space, Header("-Attack Charging Time"), SerializeField]
    private float Atk_ChargeCount;
    private float Charge_Count; //차지시간

    [Space, Header("-Attack DelayTime"), SerializeField]
    private float Atk_Delay; // 공격속도
    [SerializeField]
    private float Atk_Delay2; // 공격속도

    private bool onAttack = true;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack() //공격수행 스크립트
    {
        if (onAttack)
        {
            onAttack = false;
            ++Combo_Count;
            animator.SetBool("isAttack", true);
            animator.SetInteger("AtkCon", Combo_Count);

            if (Combo_Count > Atk_ComboCount)
            {
                ResetAttack();//콤보 초기화
            }
        }
    }

    public void OnAirAttack() // 공중 공격 스크립트
    {
        if (onAttack)
        {
            onAttack = false;
            ++Combo_Count2;
            animator.SetBool("isAttack", true);
            animator.SetInteger("AtkCon", Combo_Count2);
            if (Combo_Count2 > Atk_ComboCount2)
            {
                ResetAttack();//콤보 초기화
            }
        }
    }

    public void OnCharging() // 차지공격 스크립트
    {
        
    }

    private void ResetAttack()//콤보 초기화
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
