using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterPrefabEditor : EditorWindow
{
    private Vector2 leftScroll;
    private Vector2 rightScroll;

    private GameObject[] prefabs;
    private GameObject selectedPrefab;
    [MenuItem("MonsterMaker/Forest/Forest monster Edit")]
    public static void OpenWindow()
    {
       var windows=GetWindow<MonsterPrefabEditor>("몬스터 데이터 수정");
        //위치 x y   width, height
        windows.position = new Rect(200f,150f,1000f,500f);
    }

    private void OnEnable()
    {
        // Assets/Items/Prefabs 폴더의 모든 프리팹 가져오기
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/ForestMonster/CreateData/Prefab" });
        prefabs = new GameObject[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();//  // 가로로 UI 배치 시작

        // 왼쪽: 프리팹 목록
        leftScroll = GUILayout.BeginScrollView(leftScroll, GUILayout.Width(200));//가로 영역 200고정
        if (GUILayout.Button("새로고침"))
        {
            OnEnable();
        }
        // 여기서 프리팹 버튼들을 쭉 나열
        foreach (var prefab in prefabs)
        {
            if (prefab == null) continue;
            if (GUILayout.Button(prefab.name))
            {
                selectedPrefab = prefab;
            }
        }
        GUILayout.EndScrollView(); // 스크롤뷰 닫음

        rightScroll = GUILayout.BeginScrollView(rightScroll);
        if (selectedPrefab != null)
        {
            GUILayout.Label("선택한 프리팹: " + selectedPrefab.name, EditorStyles.boldLabel);

           

            // 선택한 프리팹의 ItemData ScriptableObject 가져오기
            var status = selectedPrefab.GetComponentInChildren<MonsterStatus>();
            var image= selectedPrefab.GetComponentInChildren<SpriteRenderer>();
            if (status != null && status.monsterData != null)
            {
               status.monsterData.Monstername = EditorGUILayout.TextField("몬스터 이름", status.monsterData.Monstername);
               status.monsterData.Hp = EditorGUILayout.FloatField("체력", status.monsterData.Hp);
               status.monsterData.Damage= EditorGUILayout.FloatField("공격력", status.monsterData.Damage);
               status.monsterData.MoveSpeed = EditorGUILayout.FloatField("이동속도", status.monsterData.MoveSpeed);
               status.monsterData.PatrolTime = EditorGUILayout.FloatField("순찰시간", status.monsterData.PatrolTime);
               status.monsterData.IdleTime = EditorGUILayout.FloatField("대기시간", status.monsterData.IdleTime);
               status.monsterData.AttackDistance = EditorGUILayout.FloatField("공격거리", status.monsterData.AttackDistance);
                if (GUILayout.Button("Save Changes"))
                {
                    EditorUtility.SetDirty(status.monsterData);
                    AssetDatabase.SaveAssets();
                }
                if (GUILayout.Button("Delete Prefab"))
                {
                    string prefabPath = AssetDatabase.GetAssetPath(selectedPrefab);
                    string dataPath = "Assets/ForestMonster/CreateData/data/" + selectedPrefab.name + ".asset";
                    AssetDatabase.DeleteAsset(prefabPath);
                    AssetDatabase.DeleteAsset(dataPath);
                    AssetDatabase.Refresh();
                    selectedPrefab = null;

                    // 왼쪽 리스트 다시 로드
                    OnEnable();
                }
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndHorizontal();// 가로 배치 닫음
    }
}
