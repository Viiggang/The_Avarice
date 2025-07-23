using System;
using System.Collections.Generic;
using UnityEngine;

 public partial class InputKey : MonoBehaviour
{
    // 키 문자(string) → 함수 매핑
    private Dictionary<KeyCode, Action> keyFunctionMap = new Dictionary<KeyCode, Action>();
    public ChainPattern chainPattern;//<--직렬화로 붙여넣기
    void Start()
    {
        // 키 매핑 등록
        keyFunctionMap[KeyCode.M] = ShowMapUI;
        keyFunctionMap[KeyCode.B] = ShowInventoryUI;
        keyFunctionMap[KeyCode.Escape] = HideLastUI;
        //keyFunctionMap["T"] = null;
        //keyFunctionMap["Y"] = null;
        //keyFunctionMap["U"] = null;
        //keyFunctionMap["I"] = null;
        //keyFunctionMap["O"] = null;
        //keyFunctionMap["P"] = null;
        //keyFunctionMap["A"] = null;
        //keyFunctionMap["S"] = null;
        //keyFunctionMap["D"] = null;
        //keyFunctionMap["F"] = null;
        //keyFunctionMap["G"] = null;
        //keyFunctionMap["H"] = null;
        //keyFunctionMap["J"] = null;
        //keyFunctionMap["J"] = null;
        //keyFunctionMap["K"] = null;
        //keyFunctionMap["L"] = null;
        //keyFunctionMap["Z"] = null;
        //keyFunctionMap["X"] = null;
        //keyFunctionMap["C"] = null;
        //keyFunctionMap["V"] = null;
        //keyFunctionMap["B"] = null;
        //keyFunctionMap["N"] = null;
        //keyFunctionMap["M"] = null;
        //keyFunctionMap["1"] = ActionSpace;
        keyFunctionMap[KeyCode.Space] = chainPattern.TypeSearch;
    }

    void Update()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                 

                // 키 매핑이 등록되어 있다면 실행
                if (keyFunctionMap.TryGetValue(key, out Action action))
                {
                    action.Invoke();
                }
            }
        }
    }

   
}
