using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColossalHandler : MonoBehaviour
{
   public IAblity ablity;
    private void init()
    {
        ablity = new ClossalAblity();
    }
    void Start()
    {
        init();
        Debug.Log(ablity.Name);
        Debug.Log(ablity.MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
