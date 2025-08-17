using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public interface IState<T>
{
    public void Enter(T manager);
    public void Update();
    public void Exit();
   
}
public class MonsterMachine<T>
{
    private IState<T> currentState;
    public void ChangeState(MonsterStates<T> newState, T manager)
    {
        // 기존 상태 종료
        currentState?.Exit();

        // 상태 변경
        currentState = newState;
       
        // 새 상태 진입
        currentState.Enter(manager);
    }
    public void Update()
    {
        currentState.Update();
    }

}
public abstract class MonsterStates<T> : ScriptableObject, IState<T>
{
    [Leein.InspectorName("Dic저장한 이름")] public string StateName;
    public virtual void Initialize(T manager) { }
    public virtual void Enter(T manager) { }
    public virtual void Update() { }
    public virtual void Exit() { }

}

[CustomEditor(typeof(MonsterStates<>), true)]
public class MonsterStatesEditor : Editor
{
    private string[] options = new string[] { "idle", "chase", "patrol", "move", "attack", "death" };
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        // 현재 상태 SO 참조
        var state = target as MonsterStates<MonsterController>; // object로 캐스팅 (편법)

        if (state == null) return;

        // 현재 StateName이 목록 중 몇 번째인지 확인
        selectedIndex = Mathf.Max(0, System.Array.IndexOf(options, state.StateName));

        // Dropdown 표시
        selectedIndex = EditorGUILayout.Popup("Dic에 저장할 이름", selectedIndex, options);

        // 선택된 값 string에 저장
        state.StateName = options[selectedIndex];

        // 값 변경 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(state);
        }

        // 기본 Inspector도 출력하려면
        DrawDefaultInspector();
    }
}