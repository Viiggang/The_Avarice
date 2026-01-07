using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUI : MonoBehaviour
{
    public GameObject obj;


    private void Start()
    {
        ColossalEvent.Instance.OnUiEvent += On;
    }    
        
     
    public void On()
    {
        obj.SetActive(true);
        Invoke("off", 2f);
    }
    public void off()
    {
        obj.SetActive(false);
    }
}
