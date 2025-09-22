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
    //���� �ɷ�ġ �ܺο��� �޾ƿ���
    public string bossName;//���� �̸� (<--�ʿ� ���� �ѵ� ���п뵵)
    public float speed = 0f;//�̵��ӵ�
    public int defense = 0;//����
    public float hp;//ü��
    public float MaxHp;
    public float HalfHp;

    public BossStage BossStage;
    private void OnEnable()
    {
        StatusInitialize();
    }

    public void StatusInitialize()
    {
        MaxHp= _Data.hp;//�ƽ�ü��
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
        // �⺻ �ν����� �׸���
        DrawDefaultInspector();

        // ��� ��������
        var data = (BossStatus)target;

        // ��ư �߰�
        if (GUILayout.Button("�ɷ�ġ ����"))
        {
            data.StatusInitialize();
        }
        if (GUILayout.Button("-100 �����"))
        {
            data.OnHitDamage(100f);
        }
    }
}