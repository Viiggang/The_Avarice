using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    public GameObject obj;


    private void Start()
    {
        this.gameObject.GetComponentInChildren<Image>().gameObject.SetActive(false);
        ColossalEvent.Instance.OnUiEvent += On;
    }    
        
     
    public void On()
    {
        obj.SetActive(true);
        Invoke("off", 2f);
    }
    public void off() => obj.SetActive(false);

}
