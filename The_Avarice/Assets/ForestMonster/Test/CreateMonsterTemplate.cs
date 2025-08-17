using UnityEngine;
using UnityEditor;

public class CreateMonsterTemplate : EditorWindow
{
    MonsterManager manager;
    MonsterAniManager aniManager;
    MonsterData data;
    GameObject Clone;
    GameObject childObj_MonsterHandler;
    Sprite imagedata;
  [MenuItem("MonsterMaker/Forest monster template")]
    public static void OpenWindow()
    {
        GetWindow<CreateMonsterTemplate>("몬스터 템플릿 생성");
    }

    private void OnGUI()
    {
        if (manager == null && aniManager == null && data == null)
        {
            if (GUILayout.Button("몬스터 생성하기"))
            {
                // ScriptableObject 생성
                data = ScriptableObject.CreateInstance<MonsterData>();
               
                // GameObject + 컴포넌트 생성
                CreateClone();
            }
            return;
        }

        GUILayout.Label("몬스터 데이터 설정", EditorStyles.boldLabel);
        data.Monstername = EditorGUILayout.TextField("몬스터 이름 설정", data.Monstername);
        data.Hp = EditorGUILayout.FloatField("몬스터 체력 설정", data.Hp);
        data.Damage = EditorGUILayout.FloatField("몬스터 공격력 설정", data.Damage);
        data.MoveSpeed = EditorGUILayout.FloatField("몬스터 이동속도 설정", data.MoveSpeed);
        data.PatrolTime = EditorGUILayout.FloatField("순찰 시간 설정", data.PatrolTime);
        data.IdleTime = EditorGUILayout.FloatField("대기 시간 설정", data.IdleTime);
        data.AttackDistance = EditorGUILayout.FloatField("공격 거리 설정", data.AttackDistance);
         imagedata = (Sprite)EditorGUILayout.ObjectField(
            "몬스터 기본 이미지",   // 라벨
            imagedata,         // 현재 값
            typeof(Sprite),        // 타입
            false                  // 씬 오브젝트 허용? false = Project 에셋만 가능
        );

        if (GUILayout.Button("Generate Prefab"))
        {
            Clone.name = data.Monstername;
            // Handler에 MonsterStatus 붙여서 데이터 참조
            MonsterStatus status = childObj_MonsterHandler.AddComponent<MonsterStatus>();
            status.monsterData = data;

            // ScriptableObject 저장
            string assetPath = $"Assets/ForestMonster/CreateData/Data/{data.Monstername}.asset";
            AssetDatabase.CreateAsset(data, assetPath);
            AssetDatabase.SaveAssets();

            // Prefab 저장
            string prefabPath = $"Assets/ForestMonster/CreateData/Prefab/{data.Monstername}.prefab";
            PrefabUtility.SaveAsPrefabAsset(Clone, prefabPath);
            EditorUtility.DisplayDialog("완료", $"{data.Monstername} 몬스터 프리팹 생성 완료!", "OK");
            DestroyImmediate(Clone);
            manager = null;
            aniManager = null;
            data = null;
            childObj_MonsterHandler = null;
        }
    }

    private void CreateClone()
    {
        // 부모 오브젝트
        Clone = new GameObject();

        // 자식 오브젝트
        childObj_MonsterHandler = new GameObject("MonsterHandler");
        childObj_MonsterHandler.transform.SetParent(Clone.transform);
        childObj_MonsterHandler.transform.localPosition = Vector3.zero;
        // 필요한 컴포넌트 추가
        manager = childObj_MonsterHandler.AddComponent<MonsterManager>();
        aniManager = childObj_MonsterHandler.AddComponent<MonsterAniManager>();
        childObj_MonsterHandler.AddComponent<BoxCollider2D>();
          childObj_MonsterHandler.GetComponent<BoxCollider2D>().isTrigger=true;

        childObj_MonsterHandler.layer = 6;
    }
}
