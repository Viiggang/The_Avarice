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

    [Leein.InspectorName("���� Flipüũ��")][SerializeField]public SpriteRenderer spriteRenderer;
    [Leein.InspectorName("���� �ǰ� �ݶ��̴�")][SerializeField]private BoxCollider2D HitBox;

    [Leein.InspectorName("���� ��ġ")] public Transform Boss_Pos;
    [Leein.InspectorName("Ÿ��")] public Transform target;
    [Leein.InspectorName("���� �����ݶ��̴�")] public BoxCollider2D Collider;
    [Leein.InspectorName("���� �ִϸ��̼�")] public Animator animator;
    [Leein.InspectorName("���� �ɷ�ġ")]public BossAblity BossAblity;

   
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
    //        Debug.Log("BossController �ߺ� �߻� �Ͽ� ������");
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
