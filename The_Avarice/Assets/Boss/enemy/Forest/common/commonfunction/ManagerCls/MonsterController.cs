using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    
    public MonsterMachine<MonsterController> MonsterMachine;//상태머신
    [HideInInspector] public string StartState;//시작 상태

    [Leein.InspectorName("몬스터 애니메이션")]
    public MonsterAniController aniManager;//애니메이션 매니저

    [Leein.InspectorName("몬스터 능력치")] 
    public MonsterStatus statusManager;//상태 매니저

    [Leein.InspectorName("몬스터 인지 범위")]
    public MsDetectionRange Detection;//인지 범위
    [Leein.InspectorName("최상위 부모 transform")]
    public Transform MonsterTrans;//최상위 위치

    [Leein.InspectorName("애니메이션 이벤트")] 
    public MonsterAniEvents MonsterAniEvent;

    [Leein.InspectorName("찾은 플레이어 트랜스폼")] 
    public Transform target;

    [SerializeField] public List<MonsterStates> StatesList;//인스펙트 창에서 상태주입하면 됨
    public Dictionary<string, MonsterStates> State;//상태관리용

  
   
    void Awake()
    {
        MonsterMachine = new MonsterMachine<MonsterController>(this);
        State = StatesList.ToDictionary(value => value.StateName, value => value);
    }

    void Start()
    {
        MonsterMachine.ChangeState(State[StartState],this);
    }
    void Update()
    {
        MonsterMachine.Update();
    }
}

[CustomEditor(typeof(MonsterController))]
public class MonsterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 대상 스크립트 가져오기
        MonsterController manager = (MonsterController)target;

        // 기본 Inspector 그리기
        DrawDefaultInspector();

        // 상태 리스트가 null 아니면 드롭다운 표시
        if (manager.StatesList != null && manager.StatesList.Count > 0)
        {
            // StateName 목록 뽑기
            var options = manager.StatesList
                .Where(s => s != null)
                .Select(s => s.StateName)
                .ToArray();

            if (options.Length > 0)
            {
                // 현재 선택된 index
                int selectedIndex = Mathf.Max(0, System.Array.IndexOf(options, manager.StartState));

                // 드롭다운 UI
                selectedIndex = EditorGUILayout.Popup("시작 상태", selectedIndex, options);

                // 선택 결과 저장
                manager.StartState = options[selectedIndex];
            }
        }

        // 값 변경되면 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }
    }
}