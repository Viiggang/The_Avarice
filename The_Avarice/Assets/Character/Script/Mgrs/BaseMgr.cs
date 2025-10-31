using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMgr<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T Instance = null;

    public static T instance
    {

        get
        {
            if (Instance == null)
            {
                GameObject obj = GameObject.Find(typeof(T).Name);

                if (obj == null)
                {
                    obj = new GameObject(typeof(T).Name);
                    Instance = obj.AddComponent<T>();
                }
                else
                {
                    Instance = obj.GetComponent<T>();
                }
            }
            return Instance;
        }

    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}


