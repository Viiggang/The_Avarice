using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class MonsterEdit : EditorWindow
{
    MonsterController controller;
    MonsterAniManager aniManager;
    MonsterData data;
    List<MonsterAniData> MonsterAniList;
    List<MonsterStates> StatesList;

    GameObject cloneRoot;
    GameObject childObj_MonsterHandler;
    GameObject animaction;
    GameObject detectionRange;
    GameObject footCollider;

    private Vector2 leftScroll;
    private Vector2 rightScroll;

    private Vector2 aniScroll;
    private Vector2 rScroll;
    private GameObject[] prefabs;
    private GameObject selectedPrefab;
    bool showStats = false;
    Sprite imagedata;

   
    [MenuItem("MonsterMaker/Forest/Forest monster Create")]
    public static void OpenWindow()
    {
        var window = GetWindow<MonsterEdit>("MonsterEdit");
        window.position = new Rect(200f, 150f, 1000f, 500f);
    }

    private void OnEnable()
    {
        // "Assets/ForestMonster/CreateData/Prefab" 폴더의 모든 프리팹 로드
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
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("몬스터 생성하기"))
        {
            selectedPrefab = null;
            imagedata = null;
            data = ScriptableObject.CreateInstance<MonsterData>();
            showStats = true;
            CreateClone();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        // 왼쪽 스크롤 - 프리팹 리스트
        leftScroll = GUILayout.BeginScrollView(leftScroll, GUILayout.Width(200));
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("몬스터 목록", EditorStyles.boldLabel);
        foreach (var prefab in prefabs)
        {
            if (prefab == null) continue;
            if (GUILayout.Button(prefab.name))
            {
                selectedPrefab = prefab;
                showStats = false;
            }
        }
      

        GUILayout.EndScrollView();

        // 오른쪽 스크롤 - 상세 UI
        rightScroll = GUILayout.BeginScrollView(rightScroll);
        GUILayout.BeginVertical();

        if (showStats)
            DrawNewMonsterEditor();

        if (selectedPrefab != null)
            DrawSelectedPrefabEditor();

        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUILayout.EndHorizontal();
    }

    void DrawNewMonsterEditor()
    {
        GUILayout.Label("몬스터 데이터 설정", EditorStyles.boldLabel);
        data.Monstername = EditorGUILayout.TextField("몬스터 이름", data.Monstername);
        data.Hp = EditorGUILayout.FloatField("체력", data.Hp);
        data.Damage = EditorGUILayout.FloatField("공격력", data.Damage);
        data.MoveSpeed = EditorGUILayout.FloatField("이동속도", data.MoveSpeed);
        data.PatrolTime = EditorGUILayout.FloatField("순찰 시간", data.PatrolTime);
        data.IdleTime = EditorGUILayout.FloatField("대기 시간", data.IdleTime);
        data.AttackDistance = EditorGUILayout.FloatField("공격 거리", data.AttackDistance);
        
        // 애니메이터 컨트롤러 설정
        if (animaction != null)
        {
            Animator animator = animaction.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField(
                    "컨트롤러 설정", animator.runtimeAnimatorController, typeof(RuntimeAnimatorController), false);
            }
        }
 
        // 기본 이미지 선택
        imagedata = (Sprite)EditorGUILayout.ObjectField("몬스터 기본 이미지", imagedata, typeof(Sprite), false);
     
        if (GUILayout.Button("Generate Prefab"))
        {
        
            GeneratePrefab();
            OnEnable();
            showStats = false;
        }
            
    }

    void DrawSelectedPrefabEditor()
    {
        GUILayout.Label("선택한 프리팹: " + selectedPrefab.name, EditorStyles.boldLabel);

        var status = selectedPrefab.GetComponentInChildren<MonsterStatus>();
        var state = selectedPrefab.GetComponentInChildren<MonsterController>();
        var objSprite = selectedPrefab.GetComponentInChildren<SpriteRenderer>();
        var animanager = selectedPrefab.GetComponentInChildren<MonsterAniManager>();
        MonsterAniList = animanager.MonsterAniList;
        StatesList = state.StatesList;
        if (status == null || status.monsterData == null)
            return;

        status.monsterData.Monstername = EditorGUILayout.TextField("몬스터 이름", status.monsterData.Monstername);
        status.monsterData.Hp = EditorGUILayout.FloatField("체력", status.monsterData.Hp);
        status.monsterData.Damage = EditorGUILayout.FloatField("공격력", status.monsterData.Damage);
        status.monsterData.MoveSpeed = EditorGUILayout.FloatField("이동속도", status.monsterData.MoveSpeed);
        status.monsterData.PatrolTime = EditorGUILayout.FloatField("순찰시간", status.monsterData.PatrolTime);
        status.monsterData.IdleTime = EditorGUILayout.FloatField("대기시간", status.monsterData.IdleTime);
        status.monsterData.AttackDistance = EditorGUILayout.FloatField("공격거리", status.monsterData.AttackDistance);
        imagedata = objSprite.sprite;
        imagedata = (Sprite)EditorGUILayout.ObjectField("몬스터 기본 이미지", imagedata, typeof(Sprite), false);
        //////////////////////////////////////////////////////////////////////////////////
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        aniScroll = GUILayout.BeginScrollView(aniScroll, GUILayout.Width(400));
        for (int i = 0; i < MonsterAniList.Count; i++)
        {
            MonsterAniList[i] = (MonsterAniData)EditorGUILayout.ObjectField($"애니메이션 트리거 리스트:{i+1}",// 라벨 (string)
                                                                                           MonsterAniList[i],   // 현재 값 (Object)
                                                                                           typeof(MonsterAniData),  // 허용 타입 (Type)
                                                                                           false);
        }
        if (GUILayout.Button("+ Add AniState"))
        {
          
            MonsterAniList.Add(new MonsterAniData());
        }
        if (GUILayout.Button("- Remove Last AniState"))
        {
            if (MonsterAniList.Count == 0) return;
           
            MonsterAniList.RemoveAt(MonsterAniList.Count-1);
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        //////////////////////////////////////////////////////////////////////////////////
        GUILayout.BeginVertical();
       
        for(int j=0; j< StatesList.Count; j++)
        {
            StatesList[j]=(MonsterStates)EditorGUILayout.ObjectField($"몬스터 상태 리스트:{j + 1}",// 라벨 (string)
                                                                                           StatesList[j],   // 현재 값 (Object)
                                                                                           typeof(MonsterStates),  // 허용 타입 (Type)
                                                                                           false);
        }
        if (GUILayout.Button("+ Add MonsterState"))
        {

            StatesList.Add(new MonsterStates());
        }
        if (GUILayout.Button("- Remove MonsterState Last Index"))
        {
            if (StatesList.Count == 0) return;
            StatesList.RemoveAt(StatesList.Count - 1);
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Save Changes"))
        {
            objSprite.sprite = imagedata;
          
           
            EditorUtility.SetDirty(status.monsterData);
            EditorUtility.SetDirty(animanager);
            EditorUtility.SetDirty(state);
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
            OnEnable();
        }
    }

    private void CreateClone()
    {
        cloneRoot = new GameObject("Monster");
        cloneRoot.AddComponent<Rigidbody2D>();

        childObj_MonsterHandler = new GameObject("MonsterHandler");
        childObj_MonsterHandler.transform.SetParent(cloneRoot.transform, false);
        childObj_MonsterHandler.layer = 6;

        var managerComp = childObj_MonsterHandler.AddComponent<MonsterController>();
        var aniManagerComp = childObj_MonsterHandler.AddComponent<MonsterAniManager>();
        var statusComp = childObj_MonsterHandler.AddComponent<MonsterStatus>();
        statusComp.AniManager = aniManagerComp;
        var handlerCollider = childObj_MonsterHandler.AddComponent<BoxCollider2D>();
        handlerCollider.isTrigger = true;

        animaction = new GameObject("Animaction");
        animaction.transform.SetParent(childObj_MonsterHandler.transform, false);
        var animator = animaction.AddComponent<Animator>();
        var spriteRenderer = animaction.AddComponent<SpriteRenderer>();

        detectionRange = new GameObject("DetectionRange");
        detectionRange.transform.SetParent(childObj_MonsterHandler.transform, false);
        var detectComp = detectionRange.AddComponent<MsDetectionRange>();
        detectComp.MonsterTrans = cloneRoot.transform;
        detectComp.renderer = spriteRenderer;
        detectComp.Collider = handlerCollider;

        footCollider = new GameObject("FootCollider");
        footCollider.transform.SetParent(childObj_MonsterHandler.transform, false);
        footCollider.AddComponent<BoxCollider2D>();

        statusComp.BoxCollider2D = handlerCollider;
        statusComp.spriteRenderer = spriteRenderer;
        aniManagerComp.animator = animator;

        managerComp.aniManager = aniManagerComp;
        managerComp.statusManager = statusComp;
        managerComp.Detectionrange = detectComp;
        managerComp.MonsterTrans = cloneRoot.transform;

        controller = managerComp;
        aniManager = aniManagerComp;
    }

    private void GeneratePrefab()
    {
        if (cloneRoot == null || data == null)
        {
            Debug.LogError("프리팹 생성 실패: cloneRoot 또는 data가 null입니다.");
            return;
        }

        cloneRoot.name = data.Monstername;

        if (imagedata != null && animaction != null)
        {
            var spriteRenderer = animaction.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.sprite = imagedata;
        }

        var status = childObj_MonsterHandler.GetComponent<MonsterStatus>();
        if (status != null)
            status.monsterData = data;

        string assetPath = $"Assets/ForestMonster/CreateData/Data/{data.Monstername}.asset";
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();

        string prefabPath = $"Assets/ForestMonster/CreateData/Prefab/{data.Monstername}.prefab";
        PrefabUtility.SaveAsPrefabAsset(cloneRoot, prefabPath);

        EditorUtility.DisplayDialog("완료", $"{data.Monstername} 몬스터 프리팹 생성 완료!", "OK");

        DestroyImmediate(cloneRoot);
        controller = null;
        aniManager = null;
        data = null;
        childObj_MonsterHandler = null;
        animaction = null;
        detectionRange = null;
        footCollider = null;
        imagedata = null;
    }
}
