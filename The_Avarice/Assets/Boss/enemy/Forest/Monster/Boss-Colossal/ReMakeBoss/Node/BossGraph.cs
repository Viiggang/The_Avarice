using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEditor;
using XNodeEditor;
using System.Linq;
[CreateAssetMenu(fileName = "BossGraph", menuName = "Graph/Colossal")]
public class BossGraph : NodeGraph
{ 
	
}
[CustomNodeGraphEditor(typeof(BossGraph))]
public class BossGraphEditor : NodeGraphEditor
{
    
}