using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPos : MonoBehaviour
{
    public SpriteRenderer flipState;
    public float rightPos;
    public float leftPos;
    // 
    public void SetBulletPos()
    {
        if(flipState.flipX)
        {
            this.transform.localPosition= new Vector2 (leftPos, 0.076f);
        }
        else
        {
            this.transform.localPosition = new Vector2(leftPos, 0.076f);
        }
    }
}
