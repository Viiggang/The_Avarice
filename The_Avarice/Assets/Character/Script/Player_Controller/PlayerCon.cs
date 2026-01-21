using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerCon : MonoBehaviour
{

    [SerializeField, Range(0.5f, 3.5f)]
    private float skill1Cooldown = 1f;
    [SerializeField, Range(0.1f, 3f)]
    private float Skill1Duration = 1f;

    [SerializeField]
    private Collider2D hitBox;
    [SerializeField]
    private GameObject ExtraHitBox1;
    [SerializeField]
    private GameObject ExtraHitBox2;

    public Dictionary<Player_Type, IpController> Skill1States;
    public Dictionary<Player_Type, IpController> Skill2States;
    [HideInInspector]
    public SpriteRenderer sprite;
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

    public float resetCooldown = 0f;
    private Collider2D currentOneWayPlatform;

    //제어용 변수
    public bool Direction { get; private set; } = true; // 바라보는 방향
    public bool CanDash { get; set; } = true;
    public bool CanSkill1 { get; set; } = true;
    public bool IsDashing { get; set; } = false;
    public bool IsSkill1 { get; set; } = false;
    public bool IsHurt { get; set; } = false;
    public bool IsDead { get; private set; } = false;
    public bool CanMove { get; set; } = true;


   
    private bool Grounded;
    private Vector2 groundNormal = Vector2.up;
    private float slopeAngle;


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

        transform.localScale = new Vector3(1.5f, 1.5f, 1);
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
        CheckGround();
    }

    #region 
    public void SetDirection(float inputX)
    {
        if (inputX < 0 && Direction)
        {
            transform.localScale = new Vector3(-1.5f, 1.54f, 1);
            Direction = false;
        }
        else if (inputX > 0 && !Direction)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            Direction = true;
        }
    }

    public void EnableHitBox(bool enable)
    {
        hitBox.enabled = enable;
    }

    public void EnableExtraHitBox1()
    {
        if (ExtraHitBox1 != null)
            ExtraHitBox1.SetActive(!ExtraHitBox1.activeSelf);
    }

    public void EnableExtraHitBox2()
    {
        if (ExtraHitBox2 != null)
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
    public void Jump2()
    {
        Rigid.velocity = new Vector2(Rigid.velocity.x, PlayerMgr.instance.JumpPower*0.5f);
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(Rigid.position, Vector2.down, 0.8f, PlayerMgr.instance.groundLayer);
        return hit.collider != null;
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, PlayerMgr.instance.groundRayDistance, PlayerMgr.instance.groundLayer);

        if (hit)
        {
            Grounded = true;
            groundNormal = hit.normal;
            slopeAngle = Vector2.Angle(groundNormal, Vector2.up);
        }
        else
        {
            Grounded = false;
            groundNormal = Vector2.up;
            slopeAngle = 0f;
        }
    }

    public void MoveHorizontally(float speed)
    {
        if (Grounded && slopeAngle > 0.1f && slopeAngle <= PlayerMgr.instance.maxSlopeAngle)
        {
            Vector2 slopeDirection = new Vector2(groundNormal.y, -groundNormal.x).normalized;

            Vector2 velocity = slopeDirection * speed;
            Debug.Log(velocity+" | " +slopeDirection + " | " + speed);
            Rigid.velocity = velocity;
        }
        else
        {
            Rigid.velocity = new Vector2(speed, Rigid.velocity.y);
        }
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

    public bool IsOnOneWayPlatform()
    {
        return currentOneWayPlatform != null && IsGrounded();
    }

    //OneWayPlatfrom체크용 충돌감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("OneWayPlatform"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                currentOneWayPlatform = collision.collider;
                break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerMgr.instance.groundLayer) == 0)
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 바닥 접촉 여부 (위쪽 법선)
            if (contact.normal.y > 0.7f)
            {
                Grounded = true;
                groundNormal = contact.normal;
                slopeAngle = Vector2.Angle(groundNormal, Vector2.up);
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == currentOneWayPlatform)
        {
            currentOneWayPlatform = null;
        }
        if (((1 << collision.gameObject.layer) & PlayerMgr.instance.groundLayer) != 0)
        {
            Grounded = false;
        }
    }

    private IEnumerator EnableOneWayPlatform(Collider2D playerCol, Collider2D platformCol)
    {
        yield return new WaitForSeconds(0.35f);

        Physics2D.IgnoreCollision(playerCol, platformCol, false);
    }

    public void DisableOneWayPlatform() //OneWayPlatfrom체크 탈출용
    {
        if (currentOneWayPlatform == null)
            return;

        Collider2D playerCollider = GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, currentOneWayPlatform, true);

        StartCoroutine(EnableOneWayPlatform(playerCollider, currentOneWayPlatform));
    }

    public float GetNormalSpeed() => PlayerMgr.instance.Move_Speed;
    public float GetJumpPower() => PlayerMgr.instance.jumpPower;
    public float GetDashSpeed() => PlayerMgr.instance.DashSpeed;
    public float GetDashDuration() => PlayerMgr.instance.DashDuration;
    public float GetSkill1Duration() => Skill1Duration;
    public float GetDashCooldown() => PlayerMgr.instance.DashCooldown;
    public float GetSkill1Cooldown() => skill1Cooldown;
    public float GetDashDodge() => PlayerMgr.instance.DashDodge;
    #endregion
}

