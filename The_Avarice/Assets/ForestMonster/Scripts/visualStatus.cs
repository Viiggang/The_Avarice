using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visualStatus : MonoBehaviour
{
    public Text name;
    public Text damage;
    public Text health;
    public MonsterData data;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (data == null) return;
        name.text = $"보스 이름:{data.Monstername}";
        damage.text = $"보스 공격력 : {data.Damage.ToString()}";
        health.text= $"보스 체력: {data.Hp.ToString()}";
    }
#endif
}
