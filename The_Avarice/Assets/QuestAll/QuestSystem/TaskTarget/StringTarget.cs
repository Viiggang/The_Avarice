using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Quest/Task/Target/String", fileName = "Target_")]
public class StringTarget : TaskTarget
{

     [SerializeField]private string value;
    public override object Value => this.value.Trim();
    public override bool IsEqual(object other)
    {
        string targetAsString = other as string;
        if(targetAsString ==null)
            return false;

        bool equal = Value.Equals(targetAsString.Trim());
        return equal;
    }

}
