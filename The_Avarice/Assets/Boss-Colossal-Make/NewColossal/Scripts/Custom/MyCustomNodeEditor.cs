using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(StartNode))]
public class MyCustomNodeEditor : NodeEditor
{

    public override void OnHeaderGUI()
    {
      
        GUI.color = Color.white; // ���ϴ� �������� ����

        GUILayout.Label(target.name, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));

      
       
    }
}
 

