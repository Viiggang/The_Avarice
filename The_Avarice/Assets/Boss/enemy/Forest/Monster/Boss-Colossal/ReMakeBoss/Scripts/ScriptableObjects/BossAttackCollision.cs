using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCollisonData", menuName = "Boss/Create/AttackCollisonData") ]
public class BossAttackCollision : ScriptableObject
{
    public Vector3 offset;
    public Vector3 Size;

    [TextArea]public string description;
}
