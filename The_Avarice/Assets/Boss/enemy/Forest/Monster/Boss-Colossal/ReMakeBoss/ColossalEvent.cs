using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ColossalEvent :Singleton<ColossalEvent>
{
    //페이즈 이벤트
    public delegate void BossPageChange();
    public event BossPageChange PageEvents;

    public BoxCollider2D BoxCollider2D;

    public void OnPageEvent()//페이즈 전환에 대해서 이벤트 등록 예정
    {
        PageEvents?.Invoke();
    }
    public delegate void EndEvents();
    public event EndEvents OnEndEvents;

    public delegate void AttackUI();    
    public event AttackUI OnUiEvent;
    public void TriggerEnd()
    {
        OnEndEvents?.Invoke(); 
    }
    public void UItrigger()
    {
        OnUiEvent?.Invoke();
    }
    #region 초기화
    private void Awake()
    {
        base.Awake();
    }
   
    private void Start()
    {
        //UItrigger();
    }
#endregion
}
#if UNITY_EDITOR
[CustomEditor(typeof(ColossalEvent))]
public class ColossalEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 그리기
        DrawDefaultInspector();

        // 대상 가져오기
        var data = (ColossalEvent)target;

        // 버튼 추가
        if (GUILayout.Button("UI 페이드인아웃"))
        {
            data.UItrigger();
        }
    }
}
#endif