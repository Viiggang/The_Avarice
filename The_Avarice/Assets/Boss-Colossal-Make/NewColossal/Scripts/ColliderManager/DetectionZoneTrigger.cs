using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZoneTrigger : MonoBehaviour
{
    public bool OnTrigger=false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
         
         
        if (OnTrigger == false && collision.tag =="Player")
        {
            Debug.Log($"OnTriggerEnter2D_{collision.name}");
            OnTrigger = true;
        }
       // Debug.Log(PlayerLayer.ToString());
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
         
        if (OnTrigger==false && collision.CompareTag("Player"))
        {
            Debug.Log($"OnTriggerStay_{collision.name}");
            OnTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTrigger = false;
    }
}
