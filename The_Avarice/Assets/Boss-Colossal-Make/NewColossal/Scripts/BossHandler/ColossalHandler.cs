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
}
