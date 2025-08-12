using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerCon : MonoBehaviour
{
    [Header("- Movement Settings")]
    [SerializeField, Range(2f, 10f)]
    private float Speed = 5f;
    [SerializeField, Range(5f, 20f)]
    private float jumpPower = 10f;

    [Space, Header("- Dash Settings")]
    [SerializeField]
    private Collider2D hitBox;
    [SerializeField, Range(20f, 50f)]
    private float dashSpeed = 30f;
    [SerializeField, Range(0.05f, 0.3f)]
    private float dashDuration = 0.1f;
    [SerializeField, Range(1f, 3f)]
    private float Skill1Duration = 1f;
    [SerializeField, Range(0.2f, 3f)]
    private float dashCooldown = 1f;
    [SerializeField, Range(0.5f, 3.5f)]
    private float skill1Cooldown = 1f;
    [SerializeField, Range(0.02f, 0.15f)]
    private float dashDodge = 0.05f;
    [SerializeField]
    private GameObject dashHitBox;

    //FSM 상태관리
    public Player_ControllMachine ControlMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public AirState AirState { get; private set; }
    public DashState DashState { get; private set; }
    public AttackState AttackState { get; private set; }
    public Skill1State Skill1State { get; private set; }

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


    public float InputX { get; private set; }
    public bool JumpInput { get; private set; }

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        Attack = GetComponent<Player_Atk>();

        // FSM 초기화
        ControlMachine = new Player_ControllMachine();
        IdleState = new IdleState(this, ControlMachine);
        MoveState = new MoveState(this, ControlMachine);
        AirState = new AirState(this, ControlMachine);
        JumpState = new JumpState(this, ControlMachine);
        DashState = new DashState(this, ControlMachine);
        AttackState = new AttackState(this, ControlMachine);
        Skill1State = new Skill1State(this, ControlMachine);

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
    }

    #region 
    public void SetDirection(float inputX)
    {
        if (inputX < 0 && Direction)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Direction = false;
        }
        else if (inputX > 0 && !Direction)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Direction = true;
        }
    }

    public void EnableHitBox(bool enable)
    {
        hitBox.enabled = enable;
    }

    public void EnableDashHitBox(bool enable)
    {
        dashHitBox.SetActive(enable);
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
        RaycastHit2D hit = Physics2D.Raycast(Rigid.position, Vector2.down, 0.4f, LayerMask.GetMask("Platform"));
        return hit.collider != null;
    }

    public void MoveHorizontally(float speed)
    {
        Rigid.velocity = new Vector2(speed, Rigid.velocity.y);
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

