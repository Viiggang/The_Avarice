using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStatusData", menuName = "BossStatusData")]
public class BossStatusData : ScriptableObject
{
    public string bossName;//보스 이름 
    public float hp;//체력
    public float speed;//이동속도
    public int defense;//방어력
    [TextArea]public string descripcion;
}
