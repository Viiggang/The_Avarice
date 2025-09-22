using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    
    public MonsterMachine<MonsterController> MonsterMachine;//���¸ӽ�
    [HideInInspector] public string StartState;//���� ����

    [Leein.InspectorName("���� �ִϸ��̼�")]
    public MonsterAniController aniManager;//�ִϸ��̼� �Ŵ���

    [Leein.InspectorName("���� �ɷ�ġ")] 
    public MonsterStatus statusManager;//���� �Ŵ���

    [Leein.InspectorName("���� ���� ����")]
    public MsDetectionRange Detection;//���� ����
    [Leein.InspectorName("�ֻ��� �θ� transform")]
    public Transform MonsterTrans;//�ֻ��� ��ġ

    [Leein.InspectorName("�ִϸ��̼� �̺�Ʈ")] 
    public MonsterAniEvents MonsterAniEvent;

    [Leein.InspectorName("ã�� �÷��̾� Ʈ������")] 
    public Transform target;

    [SerializeField] public List<MonsterStates> StatesList;//�ν���Ʈ â���� ���������ϸ� ��
    public Dictionary<string, MonsterStates> State;//���°�����

  
   
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
        // ��� ��ũ��Ʈ ��������
        MonsterController manager = (MonsterController)target;

        // �⺻ Inspector �׸���
        DrawDefaultInspector();

        // ���� ����Ʈ�� null �ƴϸ� ��Ӵٿ� ǥ��
        if (manager.StatesList != null && manager.StatesList.Count > 0)
        {
            // StateName ��� �̱�
            var options = manager.StatesList
                .Where(s => s != null)
                .Select(s => s.StateName)
                .ToArray();

            if (options.Length > 0)
            {
                // ���� ���õ� index
                int selectedIndex = Mathf.Max(0, System.Array.IndexOf(options, manager.StartState));

                // ��Ӵٿ� UI
                selectedIndex = EditorGUILayout.Popup("���� ����", selectedIndex, options);

                // ���� ��� ����
                manager.StartState = options[selectedIndex];
            }
        }

        // �� ����Ǹ� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }
    }
}