using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ColossalHandler :Singleton<ColossalHandler>
{
    public IAblity ablity;
    public ColliderManager colliderManager;
    public  AniManager animanager;
    public Transform targetPlayer;
    public Transform Boss ;
    private void init()
    {
       
        ablity = new ClossalAblity();
    }
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        init();
        Debug.Log($"���� �̸�: {ablity.Name}");
        Debug.Log($"�߽�ü��:{ablity.MaxHp}");
        Debug.Log($"ü��:{ablity.Hp}");
       
    }

   public bool IsBelowHalfHp()//���� ü�� ���� ���� ���� üũ�ϴ� �Լ�
   {
        float currentHp = ablity.Hp;
        float HalfHp = (ablity.MaxHp / 2);
        return currentHp <= HalfHp;
   }
    public bool IsDead()//���� �׾�����
    {
        return (ablity.Hp<=0);
    }
    
    public bool IsNear()//�÷��̾� ������ �ִ���
    {
         
        bool near=true;
        near = (Mathf.Abs(Boss.transform.position.x-targetPlayer.position.x)<0.3f)? true:false;  
        return near;
    }
}
