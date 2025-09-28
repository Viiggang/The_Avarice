using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
 

public abstract class baseStates<T> : ScriptableObject, IState<T>
{
    [Leein.InspectorName("Dic������ �̸�")] public string StateName;

    public virtual void Enter(T controller) { }
    public virtual void Excute(T controller) { }
    public virtual void Exit(T controller) { }

}

[CustomEditor(typeof(baseStates<>), true)]
public class MonsterStatesEditor : Editor
{
    private string[] options = new string[] { "idle", "chase", "patrol", "move", "attack", "death" };
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        // ���� ���� SO ����
        var state = target as baseStates<MonsterController>; 

        if (state == null) return;

        // ���� StateName�� ��� �� �� ��°���� Ȯ��
        selectedIndex = Mathf.Max(0, System.Array.IndexOf(options, state.StateName));

        // Dropdown ǥ��
        selectedIndex = EditorGUILayout.Popup("Dic�� ������ �̸�", selectedIndex, options);

        // ���õ� �� string�� ����
        state.StateName = options[selectedIndex];

        // �� ���� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(state);
        }

        // �⺻ Inspector�� ����Ϸ���
        DrawDefaultInspector();
    }
}