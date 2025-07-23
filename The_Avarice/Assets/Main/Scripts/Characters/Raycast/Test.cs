using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector2 direction = Vector2.right*2;
    public float distance = 3f;
    RaycastHit2D hit;
    float x;
    float y;
    float moveSpeed = 5f;
    void PlayerMove()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        Vector2 Move =new Vector2 (x, y).normalized;
        transform.Translate(Move * moveSpeed * Time.deltaTime);
    }
    void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(direction.normalized * distance);

        Gizmos.DrawLine(start, end );
    }
    void Update()
    {
        // Raycast 수행

        PlayerMove();
         
       if (Input.GetKeyDown(KeyCode.Space))
       {
            hit = Physics2D.Raycast(transform.position, direction, distance);
            if(hit.collider !=null)
            {
                string name = hit.collider.name;
                Debug.Log($"현재 오브젝트 이름 : {name}");
            }
            else
            {
                Debug.Log("ㅇ?");
            }
            
       }
         
    }
}
