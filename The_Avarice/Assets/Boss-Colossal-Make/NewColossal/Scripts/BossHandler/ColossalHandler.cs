using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public enum Stage
{
    Defult=0,
    Stage1,
    Stage2
}
public class ColossalHandler :Singleton<ColossalHandler>
{
    public IAblity ablity;
    public ColliderManager colliderManager;
    public SpriteManager spriteManager;
    public  AniManager animanager;
    public TransformManager transformManager;

    public bool Near;
    public Stage currentStage;
    public float hp = 100000;
    public const float maxhp = 100000;
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        init();
    }

   public bool IsBelowHalfHp()//보스 체력 반피 이하 인지 체크하는 함수
   {
        float currentHp = hp;// ablity.Hp;
        float HalfHp = (maxhp / 2);// (ablity.MaxHp / 2);
        return currentHp <= HalfHp;
   }
    public bool IsDead()//보스 죽었는지
    {
        return (ablity.Hp<=0);
    }
    
    public bool IsNear()//플레이어 근접에 있는지
    {

        return Near;
    }
    private void init()
    {
        currentStage = Stage.Defult;
        ablity = new ClossalAblity();
    }
}
