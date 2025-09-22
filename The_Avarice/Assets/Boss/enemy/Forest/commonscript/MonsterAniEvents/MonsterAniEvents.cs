using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BulletPoolManager//원거리
{
    public GameObject bulletPrefab;
    public BulletPos bulletPos;
    public List<GameObject> bulletPool = new List<GameObject>();
    public int poolSize;
    public SpriteRenderer monsterSprite;

    public void InitializePool()
    {
        if (bulletPrefab == null) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().monsterSprite = monsterSprite;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public void ClearPool()
    {
        foreach (GameObject bullet in bulletPool)
        {
            GameObject.Destroy(bullet);
        }
        bulletPool.Clear();
    }
}
[System.Serializable]
public class  AttackCollisionData//근거리
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rigid;
    public Vector3 offset;
    public Vector3 size;
    public LayerMask playerLayer;

    public Vector3 GetGizmoCenter()
    {
        return boxCollider.bounds.center + offset;
    }
}
public class MonsterAniEvents : MonoBehaviour
{
    public GameObject DestroyObj;
    public MonsterController Controller;

    public List<BaseAniEvent> AniEventList;
    public Dictionary<string, BaseAniEvent> DicAniEvents;

   
 
    [SerializeField] public BulletPoolManager bulletPoolManager = new BulletPoolManager();
    [SerializeField] public AttackCollisionData attackCollisionData = new AttackCollisionData();
  

    private void OnDrawGizmos()
    {
        if (attackCollisionData.boxCollider == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackCollisionData.GetGizmoCenter(), attackCollisionData.size);
    }

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        DicAniEvents = AniEventList.ToDictionary(data => data.trigger, data => data);

        if (bulletPoolManager.bulletPrefab != null) bulletPoolManager.InitializePool();
    }

    public void MonsterDeadEvent()
    {
        Debug.Log("몬스터 죽음 삭제 실행");
        bulletPoolManager?.ClearPool();
        Destroy(DestroyObj);
    }
    /*
     수정해야할 사항들
     근거리 원거리 대쉬 공격 기타등등 공격 메소드 명확하게 작성하기

     */
  
    public void ArcherAttackExeCute(string trigger)
    {
        DicAniEvents[trigger].Execute(bulletPoolManager.bulletPool, bulletPoolManager.bulletPos);
    }

    public void MonsterNextStateExecute(string trigger)
    {
        DicAniEvents[trigger].Execute(Controller);
    }

    public void SkeletonAxemanAttack(string trigger)
    {
        DicAniEvents[trigger].Execute(
            GetComponent<SpriteRenderer>(),
            attackCollisionData.boxCollider,
            ref attackCollisionData.offset,
            ref attackCollisionData.size,
            attackCollisionData.playerLayer);
    }

    public void ElectricAttack(string trigger)
    {
        DicAniEvents[trigger].Execute(
            ref attackCollisionData.offset,
            ref attackCollisionData.size,
            attackCollisionData.playerLayer);
    }

    public void DashAttack(string trigger)
    {
        DicAniEvents[trigger].Execute(Controller, attackCollisionData.rigid);
    }
}

#region 
//[CustomEditor(typeof(MonsterAniEvents))]
//public class MonsterAniEventsEditor : Editor
//{
//    private SerializedProperty AniEventList;
//    private SerializedProperty bulletPrefab;
//    private SerializedProperty bulletPool;
//    private SerializedProperty Controller;
//    private SerializedProperty DestroyObj;
//    private SerializedProperty bulletPos;
//    private SerializedProperty Player;
//    private SerializedProperty renderer;
//    private SerializedProperty BoxCollider2D;
//    private SerializedProperty PoolSize;
//    private SerializedProperty offset;
//    private SerializedProperty size;

//    private bool showBulletGroup = true;
//    private bool showObjectRefs = true;
//    private bool showCollision = true;


//    private void OnEnable()
//    {
//        AniEventList = serializedObject.FindProperty("AniEventList");
//        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
//        bulletPool = serializedObject.FindProperty("bulletPool");
//        Controller = serializedObject.FindProperty("Controller");
//        DestroyObj = serializedObject.FindProperty("DestroyObj");
//        bulletPos = serializedObject.FindProperty("bulletPos");
//        Player = serializedObject.FindProperty("Player");
//        renderer = serializedObject.FindProperty("renderer");
//        BoxCollider2D = serializedObject.FindProperty("BoxCollider");
//        PoolSize = serializedObject.FindProperty("PoolSize");
//        offset = serializedObject.FindProperty("offset");
//        size = serializedObject.FindProperty("size");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        GUIStyle boldFoldout = new GUIStyle(EditorStyles.foldout);
//        boldFoldout.fontStyle = FontStyle.Bold;
//        // ============ Bullet 관련 ============
//        showBulletGroup = EditorGUILayout.Foldout(showBulletGroup, "Bullet 설정", true, boldFoldout);
//        if (showBulletGroup)
//        {
//            EditorGUILayout.BeginVertical("box");
//            if (bulletPrefab == null)
//                return;
//            EditorGUILayout.PropertyField(bulletPrefab);
//            EditorGUILayout.PropertyField(bulletPool, true);
//            EditorGUILayout.PropertyField(PoolSize);
//            EditorGUILayout.PropertyField(bulletPos);
//            EditorGUILayout.EndVertical();
//        }

//        // ============ 오브젝트 참조 ============
//        showObjectRefs = EditorGUILayout.Foldout(showObjectRefs, "Object References", true, boldFoldout);
//        if (showObjectRefs)
//        {
//            EditorGUILayout.BeginVertical("box");
//            EditorGUILayout.PropertyField(Controller);
//            EditorGUILayout.PropertyField(DestroyObj);
//            EditorGUILayout.PropertyField(renderer);
//            EditorGUILayout.PropertyField(BoxCollider2D);
//            EditorGUILayout.EndVertical();
//        }

//        // ============ 충돌/감지 관련 ============
//        showCollision = EditorGUILayout.Foldout(showCollision, "충돌 영역 / 감지", true, boldFoldout);
//        if (showCollision)
//        {
//            EditorGUILayout.BeginVertical("box");
//            EditorGUILayout.PropertyField(Player);
//            EditorGUILayout.PropertyField(offset);
//            EditorGUILayout.PropertyField(size);
//            EditorGUILayout.EndVertical();
//        }

//        // ============ 이벤트 목록 ============
//        EditorGUILayout.Space(10);
//        EditorGUILayout.PropertyField(AniEventList, new GUIContent(" 애니메이션 이벤트"), true );


//        serializedObject.ApplyModifiedProperties();
//    }
//}
#endregion