using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using XNodeEditor;
using static UnityEditor.LightingExplorerTableColumn;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerCon : MonoBehaviour
{
    [Header("- Movement Settings")]
    [SerializeField, Range(5f, 20f)]
    private float Speed = 10f;
    [SerializeField, Range(10f, 20f)]
    private float jumpPower = 10f;

    [Space, Header("- Dash Settings")]
    [SerializeField]
    private Collider2D hitBox;
    [SerializeField, Range(25f, 50f)]
    private float dashSpeed = 30f;
    [SerializeField, Range(0.05f, 0.3f)]
    private float dashDuration = 0.2f;
    [SerializeField, Range(0.1f, 3f)]
    private float Skill1Duration = 1f;
    [SerializeField, Range(0.2f, 3f)]
    private float dashCooldown = 1f;
    [SerializeField, Range(0.5f, 3.5f)]
    private float skill1Cooldown = 1f;
    private float resetCooldown = 0f;
    [SerializeField, Range(0.02f, 0.15f)]
    private float dashDodge = 0.05f;
    [SerializeField]
    private GameObject ExtraHitBox1;
    [SerializeField]
    private GameObject ExtraHitBox2;

    public Dictionary<Player_Type, IpController> Skill1States;
    public Dictionary<Player_Type, IpController> Skill2States;

    public SpriteRenderer sprite;
    public float CharacterScale;

    //FSM 상태관리
    [field: SerializeField]
    public Player_ControllMachine ControlMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public AirState AirState { get; private set; }
    public DashState DashState { get; private set; }
    public AttackState AttackState { get; private set; }

    public LightCutState LightCutState { get; private set; }
    public ShieldState ShieldState { get; private set; }
    public ChangeState ChangeState { get; private set; }
    public TrinitySealState TrinitySealState { get; private set; }

    //컴포넌트 접근용
    public Rigidbody2D Rigid { get; private set; }
    public Animator Anim { get; private set; }
    public Collider2D Collider { get; private set; }
    public Player_Atk Attack { get; private set; }
    public Pal_LightCut LightCut { get; private set; }


    //제어용 변수
    public bool Direction { get; private set; } = true; // 바라보는 방향
    public bool CanDash { get; set; } = true;
    public bool CanSkill1 { get; set; } = true;
    public bool IsDashing { get; set; } = false;
    public bool IsSkill1 { get; set; } = false;
    public bool IsHurt { get; set; } = false;
    public bool IsDead { get; private set; } = false;
    public bool CanMove { get; set; } = true;

    private ContactFilter2D filter;
    private Collider2D[] collider2Ds = default;

    public float InputX { get; private set; }
    public bool JumpInput { get; private set; }

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        Attack = GetComponent<Player_Atk>();
        sprite = GetComponent<SpriteRenderer>();

        // FSM 초기화
        ControlMachine = new Player_ControllMachine();
        IdleState = new IdleState(this, ControlMachine);
        MoveState = new MoveState(this, ControlMachine);
        AirState = new AirState(this, ControlMachine);
        JumpState = new JumpState(this, ControlMachine);
        DashState = new DashState(this, ControlMachine);
        AttackState = new AttackState(this, ControlMachine);
        LightCutState = new LightCutState(this, ControlMachine);
        ShieldState = new ShieldState(this, ControlMachine);
        ChangeState = new ChangeState(this, ControlMachine);
        TrinitySealState = new TrinitySealState(this, ControlMachine);

        Skill1States = new Dictionary<Player_Type, IpController>
        {
            { Player_Type.Paladin, LightCutState },
            { Player_Type.WindBreaker, LightCutState },
            { Player_Type.Ignis, TrinitySealState }
        };

        Skill2States = new Dictionary<Player_Type, IpController>
        {
            { Player_Type.Paladin, ShieldState },
            { Player_Type.WindBreaker, DashState },
            { Player_Type.Ignis, ChangeState }
        };

        transform.localScale = new Vector3(CharacterScale, CharacterScale, 0f);
        filter.SetLayerMask(LayerMask.GetMask("Platform", "Stair"));
    }


    public IpController GetSkill1State()
    {
        var type = PlayerMgr.instance.getPlayerType();
        return Skill1States.TryGetValue(type, out var state) ? state : IdleState;
    }

    public IpController GetSkill2State()
    {
        var type = PlayerMgr.instance.getPlayerType();
        return Skill2States.TryGetValue(type, out var state) ? state : IdleState;
    }
    private void OnEnable()
    {
        ControlMachine.Initialize(IdleState);
    }


    private void Update()
    {
        // 입력 업데이트
        if (CanMove == true)
        {
            InputX = Input.GetAxisRaw("Horizontal");
            JumpInput = Input.GetKeyDown(KeyCode.Space);
        }
        ControlMachine.CurrentState.HandleInput();
        ControlMachine.CurrentState.LogicUpdate();
 
    }

    private void FixedUpdate()
    {
        ControlMachine.CurrentState.PhysicsUpdate();

        if (!IsGrounded() && Physics2D.OverlapCollider(Collider, results: collider2Ds, contactFilter:filter) != 0)
        {
            //Vector3 normal = hit.normal;

            //// 중력에 의한 속도의 경사면 방향 성분 제거
            //Vector3 gravityDir = Physics.gravity.normalized;
            //Vector3 slideDir = Vector3.ProjectOnPlane(gravityDir, normal);

            //// 경사면을 따라 미끄러지는 속도 성분을 줄이기
            //Vector3 velocity = rb.velocity;
            //Vector3 slideVelocity = Vector3.Project(velocity, slideDir);
            //rb.velocity = velocity - slideVelocity;
        }
    }

    #region 
    public void SetDirection(float inputX)
    {
        if (inputX < 0 && Direction)
        {
            transform.localScale = new Vector3(-CharacterScale, CharacterScale, 0f);
            Direction = false;
        }
        else if (inputX > 0 && !Direction)
        {
            transform.localScale = new Vector3(CharacterScale, CharacterScale, 0f);
            Direction = true;
        }
    }

    public void EnableHitBox(bool enable)
    {
        hitBox.enabled = enable;
    }

    public void EnableExtraHitBox1()
    {
        ExtraHitBox1.SetActive(!ExtraHitBox1.activeSelf);
    }

    public void EnableExtraHitBox2()
    {
        ExtraHitBox2.SetActive(!ExtraHitBox2.activeSelf);
    }

    public void ResetVelocityX(float factor = 0.2f)
    {
        Rigid.velocity = new Vector2(Rigid.velocity.normalized.x * factor, Rigid.velocity.y);
    }

    public void Jump()
    {
        Rigid.velocity = new Vector2(Rigid.velocity.x, jumpPower);
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(Rigid.position, Vector2.down, Collider.bounds.size.y / 2f, LayerMask.GetMask("Platform"));
        return hit.collider != null;
    }


    public void MoveHorizontally(float speed)
    {
        Rigid.velocity = new Vector2(speed, Rigid.velocity.y);
    }

    public void setSkill1Cooldown(float sum)
    {
        resetCooldown = skill1Cooldown;
        skill1Cooldown *= sum;
    }
    public void resetSkill1Cooldown()
    {
        skill1Cooldown = resetCooldown;
    }

    public void Pal_ShieldPassive()
    {
        PlayerMgr.instance.setShieldPassive();
    }

    public void Player_Death()
    {
        EnableHitBox(false);
        CanMove = false;
        Rigid.velocity = Vector2.zero;
    }

    public float GetNormalSpeed() => Speed;
    public float GetJumpPower() => jumpPower;
    public float GetDashSpeed() => dashSpeed;
    public float GetDashDuration() => dashDuration;
    public float GetSkill1Duration() => Skill1Duration;
    public float GetDashCooldown() => dashCooldown;
    public float GetSkill1Cooldown() => skill1Cooldown;
    public float GetDashDodge() => dashDodge;
    #endregion
}

