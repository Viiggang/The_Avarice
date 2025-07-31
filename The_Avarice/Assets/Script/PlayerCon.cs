using System.Collections;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    [Header("- Movement Settings")]
    [SerializeField, Range(2f, 10f)] 
    private float nomal_Speed = 5f;
    [SerializeField, Range(5f, 20f)]
    private float JumpPower = 10f;

    [Header("- Dash Settings")]
    [SerializeField]
    private Collider2D HitBox;
    [SerializeField, Range(20f, 50f)]
    private float Dash_Speed = 30f;
    [SerializeField, Range(0.05f, 0.3f)]
    private float dashDuration = 0.1f;
    [SerializeField, Range(0.2f, 3f)]
    private float dashCooldown = 1f;
    [SerializeField, Range(0.02f, 0.15f)]
    private float dashDodge = 0.05f;
    [SerializeField]
    private GameObject DashHitBox;

    // 상태 플래그
    private bool Direction = true;
    private float input_x = 0f;
    private bool input_y = false;
    private bool jump = true;

    private bool isDashing = false;
    private bool canDash = true;

    private bool donMove = false; // 움직임 제어용
    private bool hurt = false;

    private bool Dead = false;

    // 코루틴 내부 사용
    private Vector2 dashDirection;

    // 컴포넌트
    private Animator animator;
    private Rigidbody2D rigid2D;
    private Collider2D collider2D;
    private Player_Atk P_Atk;

    [SerializeField, Space]
    private ScriptableObject[] Skills;
 

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        P_Atk = GetComponent<Player_Atk>();

      /*  if(Skills.Length > 0) 
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                TryGetComponent<ScriptableObject>(out Skills[i]);
            }
        }*/
    }

    private void Update()
    {
        if (!isDashing && !hurt && !Dead)//대쉬중이 아닐때 && 공격받지않았을때 && 죽지않았을때
        {
            if (!donMove) // 움직일수있을때
            {
                // 수평 입력 처리
                input_x = Input.GetAxisRaw("Horizontal");
                input_Movement(input_x);

                if (Input.GetButtonUp("Horizontal"))
                {
                    // 미끄러짐 방지
                    rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.2f, rigid2D.velocity.y);
                }

                // 대시 입력
                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && Input.GetButton("Horizontal"))
                {
                    dashDirection = new Vector2(Direction ? 1f : -1f, 0f);
                    StartCoroutine(DashCoroutine());
                }
            }

            // 공격 입력
            if (Input.GetKeyDown(KeyCode.C))
            {
                rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.2f, rigid2D.velocity.y);
                P_Atk.input_Atk();
            }

            // 스킬입력
            if(Input.GetKey(KeyCode.A))
            {

            }
        }
    }

    private void FixedUpdate()
    {
        // 이동, 점프 물리 처리
        if (!isDashing && !donMove && !hurt && !Dead)
        {
            output_Movement();
        }

        // 땅 체크
        RaycastHit2D hit = Physics2D.Raycast(rigid2D.position, Vector2.down, 0.5f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(rigid2D.position, Vector2.down * 0.5f, Color.red);

        if (hit.collider != null)
        {
            animator.SetBool("isJump", false);
            input_y = false;
            jump = true;
        }
        else
        {
            animator.SetBool("isJump", true);
            jump = false;
        }
    }

    private void input_Movement(float input_x)
    {
        // 이동 애니메이션
        if (input_x != 0f)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        // 방향 전환
        if (!isDashing)
        {
            if (input_x < 0f && Direction)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Direction = false;
            }
            else if (input_x > 0f && !Direction)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Direction = true;
            }
        }

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            input_y = true;
        }
    }

    private void output_Movement()
    {
        // 좌우 이동
        if (input_x > 0f)
            rigid2D.velocity = new Vector2(nomal_Speed, rigid2D.velocity.y);
        else if (input_x < 0f)
            rigid2D.velocity = new Vector2(-nomal_Speed, rigid2D.velocity.y);

        // 점프
        if (input_y)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, JumpPower);
            input_y = false;
        }
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;
        donMove = true;

        HitBox.enabled = false;  // 무적 처리

        animator.SetTrigger("Dash");

        // 대시 이동
        rigid2D.velocity = dashDirection * Dash_Speed;
        rigid2D.gravityScale = 0f;
        DashHitBox.SetActive(true);

        yield return new WaitForSeconds(dashDodge);//무적시간
        HitBox.enabled = true;

        yield return new WaitForSeconds(dashDuration - dashDodge);// 대시 후 제어 복구
        DashHitBox.SetActive(false);
        rigid2D.gravityScale = 2f;
        rigid2D.velocity = Vector2.zero;
        donMove = false;
        isDashing = false;
    
        // 쿨다운
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    #region - Animator Events
    private void OnAirAtk()
    {
        donMove = true;
        rigid2D.gravityScale = 0f;
        rigid2D.velocity = Vector2.zero;
    }

    private void OffAirAtk()
    {
        donMove = false;
        rigid2D.gravityScale = 2f;
    }

    private void OutAirAtk()
    {
        donMove = true;
        rigid2D.gravityScale = 3.4f;
    }

    private void onAtk()
    {
        donMove = true;
    }

    private void offAtk()
    {
        donMove = false;
    }

    private void onHit()
    {
        hurt = true;
        rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.2f, rigid2D.velocity.y);
    }

    private void offHit()
    {
        hurt = false;
    }
    #endregion
}
