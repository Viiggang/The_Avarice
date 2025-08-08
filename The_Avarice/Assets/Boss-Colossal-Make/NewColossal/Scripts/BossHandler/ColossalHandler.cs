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
        Debug.Log($"보스 이름: {ablity.Name}");
        Debug.Log($"멕스체력:{ablity.MaxHp}");
        Debug.Log($"체력:{ablity.Hp}");
       
    }

   public bool IsBelowHalfHp()//보스 체력 반피 이하 인지 체크하는 함수
   {
        float currentHp = ablity.Hp;
        float HalfHp = (ablity.MaxHp / 2);
        return currentHp <= HalfHp;
   }
    public bool IsDead()//보스 죽었는지
    {
        return (ablity.Hp<=0);
    }
    
    public bool IsNear()//플레이어 근접에 있는지
    {
         
        bool near=true;
        near = (Mathf.Abs(Boss.transform.position.x-targetPlayer.position.x)<0.3f)? true:false;  
        return near;
    }
}
