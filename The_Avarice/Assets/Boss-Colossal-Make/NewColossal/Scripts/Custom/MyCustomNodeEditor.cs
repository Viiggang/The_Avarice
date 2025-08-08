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
      
        GUI.color = Color.white; // 원하는 색상으로 설정

        GUILayout.Label(target.name, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));

      
       
    }
}
 

