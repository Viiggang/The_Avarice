using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    //속도(물리력) 관련 함수
    [Header("-Movement")]
    public float Speed;
    public float Runing_Speed = 10f;
    public float nomal_Speed = 5f;
    public float JumpPower = 10f;
    
    //방향
    private bool Direction = true;
    
    //가로 이동
    private float Move_x = 0f;
    private float input_x = 0f;

    //점프
    private bool input_y = false;
    private bool jump = true;
    
  

    Animator animator;
    Rigidbody2D rigid2D;
    Collider2D collider2D;

    public void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        Speed = nomal_Speed;
    }

    public void Update()
    {


        
        input_x = Input.GetAxisRaw("Horizontal");
        input_Movement(input_x); // 움직임 입력처리

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.2f, rigid2D.velocity.y); //미끄러짐 방지
        }




    }
    public void FixedUpdate() //물리적 처리
    {
        //이동
        output_Movement(); // 움직임 동작처리

        //점프 판정채크
        RaycastHit2D raycastHit = Physics2D.Raycast(rigid2D.position, Vector3.down,0.5f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(rigid2D.position, Vector3.down * 0.5f, Color.red);

        if (raycastHit.collider != null)
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
    private void input_Movement(float input_x) // 이동관련
    {

        if (input_x != 0f)
        //좌우이동 애니매이션
        {
            animator.SetBool("isMove", true);
        }
        else if (!Input.GetKey(KeyCode.RightArrow) | !Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isMove", false);
        }    

        if (input_x < 0f && Direction)
        {   
            transform.localScale = new Vector3(-1, 1, 1);
            Direction = false;
        }
        else if(input_x > 0f && !Direction)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Direction = true;
        }
       

        transform.Translate(Move_x, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && jump)// 점프
        {
            if (input_y == false)
            {
                input_y = true;
            }
        }

    }

    private void output_Movement() //이동처리 함수
    {
        //좌우 이동
        if (input_x > 0f)
        {
            rigid2D.velocity = new Vector2(Speed, rigid2D.velocity.y);
        }
        else if (input_x < 0f)
        {
            rigid2D.velocity = new Vector2(-Speed, rigid2D.velocity.y);
        }

        //점프
        if(input_y == true)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, JumpPower);
        }
    }

    
}
