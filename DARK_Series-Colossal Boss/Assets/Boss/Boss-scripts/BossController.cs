using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
 

public class BossController : MonoBehaviour
{
    private IBossState currentState;
    public static BossController instance;

    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField]private BoxCollider2D HitBox;

    public Transform target;
    public BoxCollider2D Collider;
    public Animator animator;

    public readonly float MaxHp =100;
    public float HP;
    public const string BossName = "Boss";
   
    private void Awake()
    {
        HP = MaxHp;
        HitBoxSet();
        CreateSingleton();
    }

    void Start()
    {
       
        ChangeState(new SleepState());
    }

    void Update()
    {
        FlipState();
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

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("BossController 중복 발생 하여 삭제함");
            Destroy(gameObject);
        }
    }
    public void FlipState()
    {
        if (target == null) { return; }
        float dir = Mathf.Sign(target.position.x - transform.position.x);
        spriteRenderer.flipX = dir == -1f ? true : false;
    }
}
