using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] private LayerMask targetLayer;

    public void OnTriggerEnter2D(Collider2D collision)
    {
         
        if (targetLayer == (1<< collision.gameObject.layer))
        {
            Wall.SetActive(true);
            Destroy(this.gameObject);
        }
    }
     
}
