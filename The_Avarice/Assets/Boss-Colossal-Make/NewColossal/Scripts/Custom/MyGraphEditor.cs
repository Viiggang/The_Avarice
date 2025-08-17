using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;
using XNodeEditor;
[CustomNodeGraphEditor(typeof(Dead))] // ����� Graph Ÿ������ �����ϼ���
public class MyGraphEditor : NodeGraphEditor
{
    public override Color GetPortColor(NodePort port)
    {
        // ��Ʈ �̸��̳� Ÿ�Կ� ���� ���� ����
        if (port.fieldName == "output") return Color.red;
        if (port.fieldName == "input") return Color.blue;

        // �⺻ ����
        return base.GetPortColor(port);
    }
}
