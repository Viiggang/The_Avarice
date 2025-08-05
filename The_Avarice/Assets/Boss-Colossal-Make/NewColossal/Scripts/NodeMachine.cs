using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodeMachine : Singleton<NodeMachine>
{
     
    public BaseState Status { private get; set; }
    public Colossal colossal;
    private NodePort port;
    private void Awake()
    {
        base.Init();
    }
    void Start()
    {
        Status = colossal.nodes[0] as BaseState;
    }

    // Update is called once per frame
    void Update()
    {
        Status.Excute();
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
            Debug.LogWarning($"포트 {portName}에 연결된 상태 없음");
        }
    }
}
