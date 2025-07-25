using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //일반공격
{
    enum Attack_Type
    {
        Charge,
        Combo
    };
    [Space, Header("-Attack")]
    [SerializeField]
    Attack_Type AtkType;

    enum Range_Type //공격 사거리
    {
        Wide,
        Colse
    };

    [SerializeField]
    Range_Type RangeType;

    enum AniType
    {
        Idle,
        Attack,
        Combo
    }

    Animator animator;

    public GameObject[] AtkRange;// 호출할 공격 범위
    


    [Header("-Attack Combo Index"),SerializeField]
    private int Atk_ComboCount;
    private int Combo_Count;// 콤보공격 호출카운트

    [SerializeField]
    private int Atk_ComboCount2;
    private int Combo_Count2;// 콤보공격 호출카운트

    [Space, Header("-Attack Charging Time"), SerializeField]
    private float Atk_ChargeCount;
    private float Charge_Count; //차지시간

    [Space, Header("-Attack Speed Time"), SerializeField]
    private float Atk_Speed; // 공격속도
    [SerializeField]
    private float Atk_Speed2; // 공격속도

    [Space, Header("-Comdo keep Time"), SerializeField]
    private float[] Coambo_Keep1; // 콤보유지시간
    [SerializeField]
    private float[] Coambo_keep2;// 콤보유지시간

    private bool onAttack = true;
    private int Comboindex = 0;
    private float ComboTimer = 0f;

    AniType aniType = AniType.Idle;

    private Coroutine Combo_Rotation;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        input_Attack();
    }

    private void OnAttack() //공격수행 스크립트
    {
        if (onAttack)
        {
            Debug.Log("onAttack");
            onAttack = false;
            ++Combo_Count;


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
            Debug.Log("onAirAttack");
            onAttack = false;
            ++Combo_Count2;

        }
    }

    public void OnCharging() // 차지공격 스크립트
    {
        
    }

    private void ResetAttack()//콤보 초기화
    {

        Combo_Count = 0; 
        Combo_Count2 = 0;

        onAttack = true;
    }

    private void input_Attack()
    {
        Debug.Log("fuck");
        if (AtkType == Attack_Type.Charge) // 일반공격 차지
        {
            //none
            if (Input.GetKey(KeyCode.C))
            {
                OnCharging();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.C))// 일반공격 콤보
            {

                OnAttack();

            }
        }
    }
}
