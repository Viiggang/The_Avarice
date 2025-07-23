using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 중복 제거
            return;
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject); // 이 오브젝트는 씬 전환 시에도 유지
    }
    public virtual  void ThisObjectDestroy()//캐릭터 생성되면 파괴용
    {
        Destroy(gameObject);
    }
}
