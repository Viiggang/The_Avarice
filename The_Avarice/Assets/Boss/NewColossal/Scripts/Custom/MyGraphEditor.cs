using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;
using XNodeEditor;
[CustomNodeGraphEditor(typeof(Dead))] // 당신의 Graph 타입으로 변경하세요
public class MyGraphEditor : NodeGraphEditor
{
    public override Color GetPortColor(NodePort port)
    {
        // 포트 이름이나 타입에 따라 조건 설정
        if (port.fieldName == "output") return Color.red;
        if (port.fieldName == "input") return Color.blue;

        // 기본 색상
        return base.GetPortColor(port);
    }
}
