using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object _lock = new object();
    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] {typeof(T)} �ν��Ͻ��� �̹� ����Ǿ����ϴ�. ���� �������� �ʽ��ϴ�.");
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError($"[Singleton] {typeof(T)} �̱����� ���� �� �̻� �����մϴ�!");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = $"{typeof(T)} (Singleton)";

                        DontDestroyOnLoad(singleton);
                    }
                }

                return instance;
            }
        }
    }

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
