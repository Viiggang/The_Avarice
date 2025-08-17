using UnityEngine;
using UnityEditor;

public class CreateMonsterTemplate : EditorWindow
{
    MonsterManager manager;
    MonsterAniManager aniManager;
    MonsterData data;

    GameObject cloneRoot;
    GameObject childObj_MonsterHandler;
    GameObject animaction;
    GameObject detectionRange;
    GameObject footCollider;

    bool showStats = true;
    Sprite imagedata;

    [MenuItem("MonsterMaker/Forest monster template")]
    public static void OpenWindow()
    {
        GetWindow<CreateMonsterTemplate>("몬스터 템플릿 생성");
    }

    private void OnGUI()
    {
        // 초기 상태
        if (manager == null && aniManager == null && data == null)
        {
            if (GUILayout.Button("몬스터 생성하기"))
            {
                // ScriptableObject 생성
                data = ScriptableObject.CreateInstance<MonsterData>();
                CreateClone();
            }
            return;
        }

        // 📌 몬스터 데이터 Foldout
        showStats = EditorGUILayout.BeginFoldoutHeaderGroup(showStats, "스탯 정보");
        if (showStats)
        {
            GUILayout.Label("몬스터 데이터 설정", EditorStyles.boldLabel);
            data.Monstername = EditorGUILayout.TextField("몬스터 이름", data.Monstername);
            data.Hp = EditorGUILayout.FloatField("체력", data.Hp);
            data.Damage = EditorGUILayout.FloatField("공격력", data.Damage);
            data.MoveSpeed = EditorGUILayout.FloatField("이동속도", data.MoveSpeed);
            data.PatrolTime = EditorGUILayout.FloatField("순찰 시간", data.PatrolTime);
            data.IdleTime = EditorGUILayout.FloatField("대기 시간", data.IdleTime);
            data.AttackDistance = EditorGUILayout.FloatField("공격 거리", data.AttackDistance);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // 📌 애니메이터 컨트롤러 선택
        if (animaction != null)
        {
            Animator animator = animaction.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController =
                    (RuntimeAnimatorController)EditorGUILayout.ObjectField(
                        "컨트롤러 설정",
                        animator.runtimeAnimatorController,
                        typeof(RuntimeAnimatorController),
                        false
                    );
            }
        }

        // 📌 기본 이미지 선택
        imagedata = (Sprite)EditorGUILayout.ObjectField(
            "몬스터 기본 이미지",
            imagedata,
            typeof(Sprite),
            false
        );

        // 📌 프리팹 생성 버튼
        if (GUILayout.Button("Generate Prefab"))
        {
            GeneratePrefab();
        }
    }

    private void CreateClone()
    {
        // 부모 오브젝트
        cloneRoot = new GameObject("Monster");
        cloneRoot.SetActive(false);
        cloneRoot.AddComponent<Rigidbody2D>();

        // 📌 MonsterHandler (핵심 컴포넌트 보관)
        childObj_MonsterHandler = new GameObject("MonsterHandler");
        childObj_MonsterHandler.transform.SetParent(cloneRoot.transform, false);
        childObj_MonsterHandler.layer = 6;

        var managerComp = childObj_MonsterHandler.AddComponent<MonsterManager>();
        var aniManagerComp = childObj_MonsterHandler.AddComponent<MonsterAniManager>();
        var statusComp = childObj_MonsterHandler.AddComponent<MonsterStatus>();
        var handlerCollider = childObj_MonsterHandler.AddComponent<BoxCollider2D>();
        handlerCollider.isTrigger = true;

        // 📌 Animaction
        animaction = new GameObject("Animaction");
        animaction.transform.SetParent(childObj_MonsterHandler.transform, false);
        var animator = animaction.AddComponent<Animator>();
        var spriteRenderer = animaction.AddComponent<SpriteRenderer>();

        // 📌 DetectionRange
        detectionRange = new GameObject("DetectionRange");
        detectionRange.transform.SetParent(childObj_MonsterHandler.transform, false);

        var detectComp = detectionRange.AddComponent<MsDetectionRange>();
        detectComp.MonsterTrans = cloneRoot.transform;
        detectComp.renderer = spriteRenderer;
        detectComp.Collider = handlerCollider;

        // 📌 FootCollider
        footCollider = new GameObject("FootCollider");
        footCollider.transform.SetParent(childObj_MonsterHandler.transform, false);
        footCollider.AddComponent<BoxCollider2D>();

        // 📌 참조 연결
        statusComp.BoxCollider2D = handlerCollider;
        statusComp.spriteRenderer = spriteRenderer;
        aniManagerComp.animator = animator;

        managerComp.aniManager = aniManagerComp;
        managerComp.statusManager = statusComp;
        managerComp.Detectionrange = detectComp;
        managerComp.MonsterTrans = cloneRoot.transform;

        // 에디터 상태 유지
        manager = managerComp;
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

        // Handler에 데이터 참조 연결
        var status = childObj_MonsterHandler.GetComponent<MonsterStatus>();
        if (status != null) status.monsterData = data;

        // ScriptableObject 저장
        string assetPath = $"Assets/ForestMonster/CreateData/Data/{data.Monstername}.asset";
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();

        // Prefab 저장
        string prefabPath = $"Assets/ForestMonster/CreateData/Prefab/{data.Monstername}.prefab";
        PrefabUtility.SaveAsPrefabAsset(cloneRoot, prefabPath);

        EditorUtility.DisplayDialog("완료", $"{data.Monstername} 몬스터 프리팹 생성 완료!", "OK");

        // 에디터 상태 초기화
        DestroyImmediate(cloneRoot);
        manager = null;
        aniManager = null;
        data = null;
        childObj_MonsterHandler = null;
        animaction = null;
        detectionRange = null;
        footCollider = null;
        imagedata = null;
    }
}
