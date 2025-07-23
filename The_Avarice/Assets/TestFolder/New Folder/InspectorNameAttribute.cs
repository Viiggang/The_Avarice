using UnityEngine;

namespace Leein
{

    public class InspectorNameAttribute : PropertyAttribute
    {
        public string displayName;
        public InspectorNameAttribute(string name)
        {
            displayName = name;
        }
    }

}
