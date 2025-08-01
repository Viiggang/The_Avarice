
using UnityEngine;
namespace Leein
{
    public static class CustomComponent
    {
        public static T SafeGetComponent<T>(this GameObject gameObject, string ErrorMessage) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"{gameObject.name}+{ErrorMessage} ������Ʈ�� ã�� �� �����ϴ�.");
            }
            return component;
        }
        public static T SafeGetComponentInChildren<T>(this GameObject gameObject, string ErrorMessage) where T : Component
        {
            T componentChildren = gameObject.GetComponentInChildren<T>();
            if (componentChildren == null)
            {
                Debug.LogError($"{gameObject.name}+{ErrorMessage} ������Ʈ�� ã�� �� �����ϴ�.");
            }
            return componentChildren;
        }
        public static T[] SafeFindObjectsOfType<T>() where T : Object
        {
            var value = Object.FindObjectsOfType<T>();
            var type= typeof(T);
            if (value == null)
            {
                Debug.LogError($"{type} ������Ʈ�� ã�� �� �����ϴ�.");
            }
            return value;
        }

    }

}

