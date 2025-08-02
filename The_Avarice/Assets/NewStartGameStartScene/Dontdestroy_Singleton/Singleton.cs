using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �ִٸ� �ߺ� ����
            return;
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject); // �� ������Ʈ�� �� ��ȯ �ÿ��� ����
    }
    public virtual  void ThisObjectDestroy()//ĳ���� �����Ǹ� �ı���
    {
        Destroy(gameObject);
    }
}
