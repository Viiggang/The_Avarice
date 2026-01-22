using Colossal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    /*
       해당 클래스는  몬스터의 상태머신 내부에서 필요한 클래스를 접근하기 위해서 만들어 졌다. 
       void Start() => MonsterMachine.ChangeState(State[StartState], this);
       MonsterMachine <-- 몬스터의 상태머신을 바꾸는데 딕셔너리의  state <-- 안에 StartState 키의 대응되는 값을 실행하다 현재 클래스를 참조한다.
       
         흐름은 
        1.[초기화]->상태머신 할당 후 List에 있는 값들을 딕셔너리로 치환 후 저장 
        2.[시작] --> 현재 상태머신을 idle로 변경
        3.[업데이트] --> 상태머신을 계속 업데이트한다.
            
        상태머신 -->idle,attack등 매개변수로 MonsterController를 받고 있고  각 상태는 MonsterController를에서 필요한컴포넌트를 접근한다.
     */
    public MonsterMachine<MonsterController> MonsterMachine;//상태머신
    [HideInInspector] public string StartState;//시작 상태

    [Leein.InspectorName("몬스터 애니메이션")]public MonsterAniController aniManager;//애니메이션 매니저
    [Leein.InspectorName("몬스터 능력치")] public MonsterStatus statusManager;//상태 매니저
    [Leein.InspectorName("몬스터 인지 범위")]public MsDetectionRange Detection;//인지 범위
    [Leein.InspectorName("최상위 부모 transform")]public Transform MonsterTrans;//최상위 위치
    [Leein.InspectorName("애니메이션 이벤트")] public MonsterAniEvents MonsterAniEvent;//
    [Leein.InspectorName("찾은 플레이어 트랜스폼")] public Transform target;//

    [SerializeField] public List<MonsterStates> StatesList;//인스펙트 창에서 상태주입하면 됨
    public Dictionary<string, MonsterStates> State;//상태관리용



    void Awake() => Init(); //<-- 데이터 초기화 및 할당

    void Start() => MonsterMachine.ChangeState(State[StartState], this);//<--상태머신 초기 idle 설정
     
    void Update() => MonsterMachine.Update();// 상태머신을 계속 업데이트한다.


    private void Init()
    {
        ///Awake 때 초기화 등 해야할 것들
        MonsterMachine = new MonsterMachine<MonsterController>(this, statusManager); //<--몬스터 상태머신 할당 
        State = StatesList.ToDictionary(value => value.StateName, value => value); //<--Dictionary 상태 
    }

    public void isDead()
    {
        MonsterMachine.ChangeState(State["death"], this);
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