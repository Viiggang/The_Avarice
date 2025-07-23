using System;
using System.Collections.Generic;
using UnityEngine;

 public partial class InputKey : MonoBehaviour
{
    // Ű ����(string) �� �Լ� ����
    private Dictionary<KeyCode, Action> keyFunctionMap = new Dictionary<KeyCode, Action>();
    public ChainPattern chainPattern;//<--����ȭ�� �ٿ��ֱ�
    void Start()
    {
        // Ű ���� ���
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
                 

                // Ű ������ ��ϵǾ� �ִٸ� ����
                if (keyFunctionMap.TryGetValue(key, out Action action))
                {
                    action.Invoke();
                }
            }
        }
    }

   
}
