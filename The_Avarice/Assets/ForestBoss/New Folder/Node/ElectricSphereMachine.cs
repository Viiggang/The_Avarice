using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;

public class ElectricSphereMachine : Singleton<ElectricSphereMachine>
{
    public ElectricSphereGraph ElectricSphereGraph;
    private BaseState Status;
    private BaseState IdleNode;
    private NodePort port;
    void Start()
    {
        Status = ElectricSphereGraph.nodes[0] as BaseState;
        IdleNode = ElectricSphereGraph.nodes[1] as BaseState;
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
            Status?.Exit();
            Status = IdleNode;
            Status?.Enter();
            //Debug.LogWarning($"포트 {portName}에 연결된 상태 없음");
        }
    }
}

