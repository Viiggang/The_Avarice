using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomNodeEditor(typeof(StartNode))]
public class MyCustomNodeEditor : NodeEditor
{

    public override void OnHeaderGUI()
    {
      
        GUI.color = Color.green; // ���ϴ� �������� ����

        GUILayout.Label(target.name, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));

      
       
    }
}
