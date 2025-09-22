using UnityEditor;
using UnityEngine;

public class MonsterManagerEditorWindow : EditorWindow
{
    private MonsterController targetManager;
    private SerializedObject serializedManager;

    [MenuItem("Window/Monster Manager Editor")]
    public static void OpenWindow()
    {
        GetWindow<MonsterManagerEditorWindow>("Monster Manager Editor");
    }

    private void OnGUI()
    {
        // 1. MonsterManager ��ü �巡�� & ���
        targetManager = (MonsterController)EditorGUILayout.ObjectField(
            "Target Monster", targetManager, typeof(MonsterController), true);

        if (targetManager != null)
        {
            // 2. SerializedObject ���� (�� ���� �����ϰ�)
            serializedManager = new SerializedObject(targetManager);

            // 3. �� ǥ�� �� ����
            serializedManager.Update();

            EditorGUILayout.PropertyField(serializedManager.FindProperty("StartState"), new GUIContent("Start State"));

            EditorGUILayout.PropertyField(serializedManager.FindProperty("aniManager"), new GUIContent("Ani Manager"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("statusManager"), new GUIContent("Status Manager"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("Detectionrange"), new GUIContent("Detection Range"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("MonsterTrans"), new GUIContent("Monster Transform"));

            // 4. ���� ���� ����
            serializedManager.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("MonsterManager ��ü�� �����ϼ���.", MessageType.Info);
        }
    }
}
