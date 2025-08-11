using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodeMachine : Singleton<NodeMachine>
{
     
    public BaseState Status { private get; set; }
    public BaseState firstnode { get; set; }
    public ColossalGraph colossalGraph;
    private NodePort port;
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        firstnode = colossalGraph.nodes[2] as BaseState;
        Status = colossalGraph.nodes[0] as BaseState;
    }

    // Update is called once per frame
    void Update()
    {
        Status?.Excute();
    } 
    
    public void SetNextState(string portName)
    {
        if (Status == null) return;

         port = Status.GetOutputPort(portName);
        if (port != null && port.IsConnected)
        {
            Status?.Exit();
            Status = port.Connection.node as BaseState;
            Status?.Enter();
        }
        else
        {
            Status?.Exit();
            Status = firstnode;
            Status?.Enter();
            //Debug.LogWarning($"포트 {portName}에 연결된 상태 없음");
        }
    }
}
