using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeBoss : MonoBehaviour
{
    [SerializeField]private BoxCollider2D collider;
    [SerializeField] private GameObject[]  bossObject;
    [SerializeField] private string playerTagName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.tag == playerTagName;
        if (isPlayer)
        {
           foreach( GameObject obj in bossObject )
            {
                obj.SetActive(true);
            }
        }
    }
}
