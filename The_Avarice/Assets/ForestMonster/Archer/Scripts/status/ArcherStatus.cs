using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStatus : MonoBehaviour
{
    [Leein.InspectorName("해골 궁수 데이터")][SerializeField]private MonsterData ArcherData;
    [Leein.InspectorName("몬스터 이름")]public string Name;
    [Leein.InspectorName("몬스터 체력")] public float Hp;
    [Leein.InspectorName("몬스터 공격력")] public float Damage;
    [Leein.InspectorName("몬스터 이동속도")] public float MoveSpeed;
    [Leein.InspectorName("몬스터 순찰시간")] public float PatrolTime;
    [Leein.InspectorName("몬스터 대기시간")] public float IdleTime;
    [Leein.InspectorName("몬스터 공격거리")] public float AttackDistance;
    [Leein.InspectorName("기즈모:활성/비활성")][SerializeField]private bool GizmosFloag = true;
  
    private void OnDrawGizmos()
    {
        if (GizmosFloag && ArcherData == null) return;
        Name= ArcherData.Monstername;
        Hp= ArcherData.Hp;
        Damage= ArcherData.Damage;
        MoveSpeed = ArcherData.MoveSpeed;
        PatrolTime= ArcherData.PatrolTime;
        IdleTime = ArcherData.IdleTime;
        AttackDistance=ArcherData.AttackDistance;
    }
    void Start()
    {
        GizmosFloag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
