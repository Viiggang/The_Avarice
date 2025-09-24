using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerCon : MonoBehaviour
{


    [Space, Header("Extra Settings")]
    [SerializeField]
    private Collider2D hitBox;

    [SerializeField]
    private GameObject ExtraHitBox1;
    [SerializeField]
    private GameObject ExtraHitBox2;

    public Dictionary<Player_Type, IpController> Skill1States;
    public Dictionary<Player_Type, IpController> Skill2States;

    [HideInInspector]
    public Player_ControllMachine ControlMachine { get; private set; }
    //FSM 상태관리
    [field: SerializeField]
   
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

    }


    public IpController GetSkill1State()
    {
        var type = PlayerMgr.instance.playerType;
        return Skill1States.TryGetValue(type, out var state) ? state : IdleState;
    }

    public IpController GetSkill2State()
    {
        var type = PlayerMgr.instance.playerType;
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
    }

    #region 
    public void SetDirection(float inputX)
    {
        if (inputX < 0 && PlayerMgr.instance.Direction)
        {
            transform.localScale = new Vector3(-0.64f, 0.64f, 0.64f);
            PlayerMgr.instance.Direction = false;
        }
        else if (inputX > 0 && !PlayerMgr.instance.Direction)
        {
            transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
            PlayerMgr.instance.Direction = true;
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
        Rigid.velocity = new Vector2(Rigid.velocity.x, PlayerMgr.instance.JumpPower);
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

    public void setSkill1Cooldown(float sum)
    {
        PlayerMgr.instance.ResetCooldown = PlayerMgr.instance.Skill1_Cooldown;
        PlayerMgr.instance.Skill1_Cooldown *= sum;
    }
    public void resetSkill1Cooldown()
    {
        PlayerMgr.instance.Skill1_Cooldown = PlayerMgr.instance.ResetCooldown;
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

    public float GetNormalSpeed() => PlayerMgr.instance.MoveSpeed;
    public float GetJumpPower() => PlayerMgr.instance.JumpPower;
    public float GetDashSpeed() => PlayerMgr.instance.DashSpeed;
    public float GetDashDuration() => PlayerMgr.instance.DashDuration;
    public float GetSkill1Duration() => PlayerMgr.instance.Skill1_Duration;
    public float GetDashCooldown() => PlayerMgr.instance.DashCooldown;
    public float GetSkill1Cooldown() => PlayerMgr.instance.Skill1_Cooldown;
    public float GetDashDodge() => PlayerMgr.instance.DashDodge;
    #endregion
}

