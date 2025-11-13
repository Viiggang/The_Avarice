using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnScriptLoaded : MonoBehaviour
{
    public static event Action<Type> onScriptEnabled;
    public static event Action<Type> onScriptDisabled;

    private static readonly HashSet<Type> activeTypes = new HashSet<Type>();

    // 스크립트 활성화시 자동 호출
    protected virtual void OnEnable()
    {
        var t = this.GetType();
        activeTypes.Add(t);
        onScriptEnabled?.Invoke(t);
    }

    // 스크립트 비활성화지 자동 호출
    protected virtual void OnDisable()
    {
        var t = this.GetType();
        activeTypes.Remove(t);
        onScriptDisabled?.Invoke(t);
    }

    // 활성화 됐는지 확인
    public static bool IsActive<T>() where T : OnScriptLoaded
    {
        return activeTypes.Contains(typeof(T));
    }

    /// <summary>
    // 스크립트 활성화 대기 코루틴
    //
    // 사용방법:
    //
    //  StartCoroutine(OnScriptLoaded.WaitUntilActive<검사 스크립트>(() => { 검사 스크립트가 활성화 되고 난 후 실행할 코드 }));
    //
    /// </summary>
    public static IEnumerator WaitUntilActive<T>(Action onActive)
        where T : OnScriptLoaded
    {
        if (IsActive<T>())
        {
            onActive?.Invoke();
            yield break;
        }

        bool done = false;

        void Handler(Type type)
        {
            if (type == typeof(T))
            {
                done = true;
                onActive?.Invoke();
            }
        }

        onScriptEnabled += Handler;

        yield return new WaitUntil(() => done);

        onScriptEnabled -= Handler;
    }
}
