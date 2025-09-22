using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

 

public class BossStateMachine : MonoBehaviour
{
    public BaseState<BossController> Status;//<--이걸로 컨트롤
    public BaseState<BossController> defaultNode;//중간 다음 연결된 노드가 없다면 idle로 복귀하게 설정
    public BaseState<BossController> deathNode;
    public BossGraph colossalGraph;
    public BossController controller;
    public MsDetectionRange PlayerTransform;
    private void Awake()
    {
        Status = colossalGraph.nodes.OfType<BossAwake>().FirstOrDefault();
        defaultNode = colossalGraph.nodes.OfType<BossIdle>().FirstOrDefault();
        deathNode = colossalGraph.nodes.OfType<BossDeath>().FirstOrDefault();

    }
    private void OnEnable()
    {
        Status.Enter(controller);
    }
    

    // Update is called once per frame
    void Update()  
    {
            if (PlayerTransform.findcollider == null)
            return;
        Status?.Excute(controller);
    }
    public void SetNextState(string portName)
    {
        if (Status == null) return;

        var port = Status.GetOutputPort(portName);
        if (port != null && port.IsConnected)//값을 가지고 있으면 
        {
            Status.Exit(controller);
            Status  = port.Connection.node as BaseState<BossController>;
            Status?.Enter(controller);
        }
        else//다음노드가 연결이 되어 있지 않다면 idle의 위치를 저장했던 데이터를 참조함
        {
            Status?.Exit(controller);
            Status = defaultNode;
            Status?.Enter(controller);
            
        }
    }
    public void SetNextState(string portName, string targetNodeName)
    {
        if (Status == null) return;

        var port = Status.GetOutputPort(portName);
        if (port != null && port.IsConnected)
        {
            foreach (var connection in port.GetConnections())
            {
                var nextNode = connection.node as BaseState<BossController>;
                if (nextNode != null && nextNode.name == targetNodeName)
                {
                    Status.Exit(controller);
                    Status = nextNode;
                    Status.Enter(controller);
                    return;
                }
            }
        }

        // fallback: 연결 안 됐거나 조건 불일치 → idle 복귀
        Status?.Exit(controller);
        Status = defaultNode;
        Status?.Enter(controller);
    }
    public void SetStateToDeath()
    {
        Status = deathNode;
        Status.Enter(controller);
        Status = null;
    }
}
