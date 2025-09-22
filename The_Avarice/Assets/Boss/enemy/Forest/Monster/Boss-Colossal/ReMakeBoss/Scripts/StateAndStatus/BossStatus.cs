using System.Collections;
using System.Collections.Generic;
using Colossal;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
public class BossStatus : MonoBehaviour,IDamage
{
    [SerializeField]private BossStatusData _Data;
    [SerializeField]private BossStateMachine Machine;
    //보스 능력치 외부에서 받아오기
    public string bossName;//보스 이름 (<--필요 없긴 한데 구분용도)
    public float speed = 0f;//이동속도
    public int defense = 0;//방어력
    public float hp;//체력
    public float MaxHp;
    public float HalfHp;

    public BossStage BossStage;
    private void OnEnable()
    {
        StatusInitialize();
    }

    public void StatusInitialize()
    {
        MaxHp= _Data.hp;//맥스체력
        speed = _Data.speed;
        defense = _Data.defense;
        hp = _Data.hp;
        bossName = _Data.bossName;
        HalfHp = (MaxHp / 2f);
    }
   
    public void OnHitDamage(float Damage)
    {
        hp -= Damage;
        UIManager.Instance.EventExecute();
        bool isDeath = hp <= 0;
        if(hp<= HalfHp)
        {
            BossStage.bossStage = Stage.Stage2;
        }
        if (isDeath)
        {
            Machine.SetStateToDeath();
        }

    }

}
[CustomEditor(typeof(BossStatus))]
public class BossStatusEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 그리기
        DrawDefaultInspector();

        // 대상 가져오기
        var data = (BossStatus)target;

        // 버튼 추가
        if (GUILayout.Button("능력치 리셋"))
        {
            data.StatusInitialize();
        }
        if (GUILayout.Button("-100 대미지"))
        {
            data.OnHitDamage(100f);
        }
    }
}