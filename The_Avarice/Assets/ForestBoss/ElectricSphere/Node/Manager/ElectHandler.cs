using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectHandler :  Singleton<ElectHandler>
{
    public ElectTransfrom TransfromManager;
    public ElectAniManager ElectAniManager;
    private void Awake()
    {
        base.Awake();
    }
}
