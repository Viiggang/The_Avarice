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
        // 1. MonsterManager 객체 드래그 & 드롭
        targetManager = (MonsterController)EditorGUILayout.ObjectField(
            "Target Monster", targetManager, typeof(MonsterController), true);

        if (targetManager != null)
        {
            // 2. SerializedObject 생성 (값 변경 가능하게)
            serializedManager = new SerializedObject(targetManager);

            // 3. 값 표시 및 편집
            serializedManager.Update();

            EditorGUILayout.PropertyField(serializedManager.FindProperty("StartState"), new GUIContent("Start State"));

            EditorGUILayout.PropertyField(serializedManager.FindProperty("aniManager"), new GUIContent("Ani Manager"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("statusManager"), new GUIContent("Status Manager"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("Detectionrange"), new GUIContent("Detection Range"));
            EditorGUILayout.PropertyField(serializedManager.FindProperty("MonsterTrans"), new GUIContent("Monster Transform"));

            // 4. 변경 사항 적용
            serializedManager.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("MonsterManager 객체를 선택하세요.", MessageType.Info);
        }
    }
}
