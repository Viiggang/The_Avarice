using Boss_Colossal;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
 

public class BossController : Singleton<BossController>
{
    private IBossState currentState;
    //public static BossController instance;

    [Leein.InspectorName("보스 Flip체크용")][SerializeField]public SpriteRenderer spriteRenderer;
    [Leein.InspectorName("보스 피격 콜라이더")][SerializeField]private BoxCollider2D HitBox;

    [Leein.InspectorName("보스 위치")] public Transform Boss_Pos;
    [Leein.InspectorName("타겟")] public Transform target;
    [Leein.InspectorName("보스 발판콜라이더")] public BoxCollider2D Collider;
    [Leein.InspectorName("보스 애니메이션")] public Animator animator;
    [Leein.InspectorName("보스 능력치")]public BossAblity BossAblity;

   
    private void Awake()
    {
       
        HitBoxSet();
        base.Awake();
        //CreateSingleton();
    }

    void Start()
    {
       
        ChangeState(new SleepState());
    }

    void Update()
    {
        
       
            currentState?.Execute(this);
       
      
    }

    public void ChangeState(IBossState newState)
    {
       
      
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

  

    private void HitBoxSet()
    {
        float offset_x = -0.06602305f;
        float offset_y = -0.05332156f;

        float Size_x = 0.6748698f;
        float Size_y = 0.6786369f;
        HitBox.offset = new Vector2(offset_x, offset_y);
        HitBox.size = new Vector2(Size_x, Size_y);
        HitBox.isTrigger = true;

    }

    //private void CreateSingleton()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Debug.Log("BossController 중복 발생 하여 삭제함");
    //        Destroy(gameObject);
    //    }
    //}
    public void FlipState()
    {
        if (target == null) { return; }
        float dir = Mathf.Sign(target.position.x - transform.position.x);
        spriteRenderer.flipX = dir == -1f ? true : false;
    }
}
