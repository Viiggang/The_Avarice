using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Atk : MonoBehaviour //�Ϲݰ���
{
    enum Attack_Type
    {
        Charge,
        Combo
    };
    [Space, Header("-Attack")]
    [SerializeField]
    Attack_Type AtkType;

    enum Range_Type //���� ��Ÿ�
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

    public GameObject[] AtkRange;// ȣ���� ���� ����
    


    [Header("-Attack Combo Index"),SerializeField]
    private int Atk_ComboCount;
    private int Combo_Count;// �޺����� ȣ��ī��Ʈ

    [SerializeField]
    private int Atk_ComboCount2;
    private int Combo_Count2;// �޺����� ȣ��ī��Ʈ

    [Space, Header("-Attack Charging Time"), SerializeField]
    private float Atk_ChargeCount;
    private float Charge_Count; //�����ð�

    [Space, Header("-Attack Speed Time"), SerializeField]
    private float Atk_Speed; // ���ݼӵ�
    [SerializeField]
    private float Atk_Speed2; // ���ݼӵ�

    [Space, Header("-Comdo keep Time"), SerializeField]
    private float[] Coambo_Keep1; // �޺������ð�
    [SerializeField]
    private float[] Coambo_keep2;// �޺������ð�

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

    private void OnAttack() //���ݼ��� ��ũ��Ʈ
    {
        if (onAttack)
        {
            Debug.Log("onAttack");
            onAttack = false;
            ++Combo_Count;


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
            Debug.Log("onAirAttack");
            onAttack = false;
            ++Combo_Count2;

        }
    }

    public void OnCharging() // �������� ��ũ��Ʈ
    {
        
    }

    private void ResetAttack()//�޺� �ʱ�ȭ
    {

        Combo_Count = 0; 
        Combo_Count2 = 0;

        onAttack = true;
    }

    private void input_Attack()
    {
        Debug.Log("fuck");
        if (AtkType == Attack_Type.Charge) // �Ϲݰ��� ����
        {
            //none
            if (Input.GetKey(KeyCode.C))
            {
                OnCharging();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.C))// �Ϲݰ��� �޺�
            {

                OnAttack();

            }
        }
    }
}
