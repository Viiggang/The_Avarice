using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

 

public class BossStateMachine : MonoBehaviour
{
    public BaseState<BossController> Status;//<--�̰ɷ� ��Ʈ��
    public BaseState<BossController> defaultNode;//�߰� ���� ����� ��尡 ���ٸ� idle�� �����ϰ� ����
    public BaseState<BossController> deathNode;
    public BossGraph colossalGraph;
    public BossController controller;
 
 
    private void Awake()
    {
        InitializeNodes();

    }
    private void OnEnable()
    {
        Status.Enter(controller);
    }
    void Update()  
    {
      if (controller.TargetPos == null)
            return;
        Status?.Excute(controller);
    }


    public void SetNextState(string portName)
    {
        if (Status == null) return;

        var port = Status.GetOutputPort(portName);
        if (port != null && port.IsConnected)//���� ������ ������ 
        {
            Status.Exit(controller);
            Status  = port.Connection.node as BaseState<BossController>;
            Status?.Enter(controller);
        }
        else//������尡 ������ �Ǿ� ���� �ʴٸ� idle�� ��ġ�� �����ߴ� �����͸� ������
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

        // fallback: ���� �� �ưų� ���� ����ġ �� idle ����
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
    private void InitializeNodes()
    {
        Status = colossalGraph.nodes.OfType<BossAwake>().FirstOrDefault();
        defaultNode = colossalGraph.nodes.OfType<BossIdle>().FirstOrDefault();
        deathNode = colossalGraph.nodes.OfType<BossDeath>().FirstOrDefault();
    }

}
