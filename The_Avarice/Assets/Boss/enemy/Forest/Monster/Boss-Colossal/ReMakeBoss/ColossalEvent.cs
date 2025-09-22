using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColossalEvent :Singleton<ColossalEvent>
{
    //������ �̺�Ʈ
    public delegate void BossPageChange();
    public event BossPageChange PageEvents;

    public BoxCollider2D BoxCollider2D;

    public void OnPageEvent()//������ ��ȯ�� ���ؼ� �̺�Ʈ ��� ����
    {
        PageEvents?.Invoke();
    }
    public delegate void EndEvents();
    public event EndEvents OnEndEvents;

    public delegate void AttackUI();    
    public event AttackUI OnUiEvent;
    public void TriggerEnd()
    {
        OnEndEvents?.Invoke();  // null üũ�ϸ鼭 ����
    }
    public void UItrigger()
    {
        OnUiEvent?.Invoke();
    }
    #region �ʱ�ȭ
    private void Awake()
    {
        base.Awake();
    }
   
    private void Start()
    {
        UItrigger();
    }
#endregion
}
[CustomEditor(typeof(ColossalEvent))]
public class ColossalEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �׸���
        DrawDefaultInspector();

        // ��� ��������
        var data = (ColossalEvent)target;

        // ��ư �߰�
        if (GUILayout.Button("UI ���̵��ξƿ�"))
        {
            data.UItrigger();
        }
    }
}