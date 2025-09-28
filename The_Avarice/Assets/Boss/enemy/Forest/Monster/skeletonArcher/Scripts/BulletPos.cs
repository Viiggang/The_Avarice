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
        this.transform.localPosition = flipState.flipX ? new Vector2(leftPos, 0.076f) : new Vector2(rightPos, 0.076f);
       
    }
}
