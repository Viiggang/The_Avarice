using QuestSystem;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestMakerTool : EditorWindow
{
    private List<Quest> questList = new List<Quest>();
    private Vector2 scrollPos;

    private Quest selectedQuest;
    private SerializedObject serializedQuest;
    private bool isCreateMode;

    private string newQuestName = "NewQuest";
    private string questSaveFolder = "Assets/QuestAll/QuestData/Resources"; // 퀘스트 저장 폴더

    [MenuItem("QuestMaker/Quest")]
    public static void ShowWindow()
    {
        GetWindow<QuestMakerTool>("Quest Maker");
    }

    private void OnEnable()
    {
        LoadAllQuests();
    }

    // 모든 Quest ScriptableObject 불러오기
    private void LoadAllQuests()
    {
        questList.Clear();

        string[] guids = AssetDatabase.FindAssets("t:Quest");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Quest quest = AssetDatabase.LoadAssetAtPath<Quest>(path);
            if (quest != null)
                questList.Add(quest);
        }

        // 삭제 후 선택 초기화
        if (selectedQuest != null && !questList.Contains(selectedQuest))
        {
            selectedQuest = null;
            serializedQuest = null;
        }

        Debug.Log($"총 {questList.Count}개의 퀘스트 로드 완료.");
    }

    private void OnGUI()
    {
        DrawToolbar();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        if (!isCreateMode)
            DrawQuestList();

        DrawRightPanel();

        EditorGUILayout.EndHorizontal();
    }

    // 상단 모드 버튼
    private void DrawToolbar()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Toggle(!isCreateMode, "퀘스트 수정", EditorStyles.toolbarButton))
            isCreateMode = false;
        if (GUILayout.Toggle(isCreateMode, "퀘스트 생성", EditorStyles.toolbarButton))
            isCreateMode = true;
        GUILayout.EndHorizontal();
    }

    // 왼쪽: 퀘스트 목록
    private void DrawQuestList()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(220));
        GUILayout.Label("퀘스트 목록", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if (quest == null) continue; // 삭제된 참조 무시

            GUIStyle style = (quest == selectedQuest) ? EditorStyles.miniButtonMid : EditorStyles.miniButton;
            if (GUILayout.Button(quest.name, style))
            {
                selectedQuest = quest;
                serializedQuest = new SerializedObject(quest);
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.Space(5);
        if (GUILayout.Button("새로고침"))
        {
            LoadAllQuests();
        }

        EditorGUILayout.EndVertical();
    }

    // 오른쪽: 수정 또는 생성 패널
    private void DrawRightPanel()
    {
        EditorGUILayout.BeginVertical();

        if (isCreateMode)
            DrawQuestCreator();
        else
            DrawQuestEditor();

        EditorGUILayout.EndVertical();
    }

    // ───────────────
    // 퀘스트 수정 GUI
    // ───────────────
    private void DrawQuestEditor()
    {
        if (selectedQuest == null)
        {
            GUILayout.Label("왼쪽에서 퀘스트를 선택하세요.", EditorStyles.helpBox);
            return;
        }

        GUILayout.Label($"[{selectedQuest.name}] 퀘스트 정보", EditorStyles.boldLabel);

        serializedQuest.Update();

        SerializedProperty prop = serializedQuest.GetIterator();
        prop.NextVisible(true);
        while (prop.NextVisible(false))
        {
            if (prop.name != "m_Script")
                EditorGUILayout.PropertyField(prop, true);
        }

        serializedQuest.ApplyModifiedProperties();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        // 저장 버튼
        if (GUILayout.Button("저장하기", GUILayout.Height(25)))
        {
            EditorUtility.SetDirty(selectedQuest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"{selectedQuest.name} 저장 완료!");
        }

        // 삭제 버튼
        if (GUILayout.Button("삭제하기", GUILayout.Height(25)))
        {
            if (EditorUtility.DisplayDialog("퀘스트 삭제",
                $"정말 {selectedQuest.name} 퀘스트를 삭제하시겠습니까?", "삭제", "취소"))
            {
                string path = AssetDatabase.GetAssetPath(selectedQuest);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log($"{selectedQuest.name} 삭제 완료!");

                selectedQuest = null;
                serializedQuest = null;
                LoadAllQuests();
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    // ───────────────
    // 퀘스트 생성 GUI
    // ───────────────
    private void DrawQuestCreator()
    {
        GUILayout.Label("새 퀘스트 생성", EditorStyles.boldLabel);

        newQuestName = EditorGUILayout.TextField("퀘스트 이름", newQuestName);
        questSaveFolder = EditorGUILayout.TextField("저장 폴더", questSaveFolder);

        GUILayout.Space(10);

        if (GUILayout.Button("퀘스트 생성", GUILayout.Height(30)))
        {
            CreateNewQuestAsset();
        }
    }

    // ScriptableObject 생성
    private void CreateNewQuestAsset()
    {
        if (string.IsNullOrEmpty(newQuestName))
        {
            EditorUtility.DisplayDialog("오류", "퀘스트 이름을 입력하세요.", "확인");
            return;
        }

        // 폴더 자동 생성
        if (!AssetDatabase.IsValidFolder(questSaveFolder))
        {
            string[] parts = questSaveFolder.Split('/');
            string currentPath = "";
            for (int i = 0; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i])) continue;
                string nextPath = string.IsNullOrEmpty(currentPath) ? parts[i] : $"{currentPath}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    string parent = string.IsNullOrEmpty(currentPath) ? "Assets" : currentPath;
                    AssetDatabase.CreateFolder(parent, parts[i]);
                }
                currentPath = nextPath;
            }
        }

        string path = $"{questSaveFolder}/{newQuestName}.asset";
        Quest newQuest = ScriptableObject.CreateInstance<Quest>();
        AssetDatabase.CreateAsset(newQuest, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"새 퀘스트 생성: {path}");

        LoadAllQuests();

        selectedQuest = newQuest;
        serializedQuest = new SerializedObject(newQuest);
        isCreateMode = false;
    }
}
