using UnityEditor;
using UnityEngine;

namespace Leein
{

    [CustomPropertyDrawer(typeof(InspectorNameAttribute))]
    public class InspectorNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InspectorNameAttribute attr = (InspectorNameAttribute)attribute;
            label.text = attr.displayName;
            EditorGUI.PropertyField(position, property, label);
        }
    }

}
