using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColossalHandler :Singleton<ColossalHandler>
{
    public IAblity ablity;
    public ColliderManager colliderManager;
    public  AniManager animanager;
    private void init()
    {
        
        ablity = new ClossalAblity();
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
}
