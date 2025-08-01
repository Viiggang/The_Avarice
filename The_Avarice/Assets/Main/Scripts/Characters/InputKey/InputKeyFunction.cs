using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class InputKey
{
    private static Stack<GameObject> uiStack = new Stack<GameObject>();
    Stack<GameObject> tempStack = new Stack<GameObject>();
    public GameObject uiRoot;

    public void ShowMapUI() => ShowUI("inven");
    public void ShowInventoryUI() => ShowUI("Image (1)");

    public void ShowUI(string name)
    {

        var target = FindUIByName(name);
        if (target.gameObject.activeSelf)
        {
            target.gameObject.SetActive(false);
            

            while (uiStack.Count > 0)
            {
               var value = uiStack.Pop();
                if (value != target)
                    tempStack.Push(value);
            }
            while (tempStack.Count > 0)
            {
                uiStack.Push(tempStack.Pop());
            }
        }
        else if (target != null && !uiStack.Contains(target))
        {
            target.SetActive(true);
            uiStack.Push(target);
        }
    }

    public void HideLastUI()
    {
        if (uiStack.Count > 0)
        {
            var lastUI = uiStack.Pop();
            lastUI.SetActive(false);
        }
    }

    private GameObject FindUIByName(string name)
    {
        var all = uiRoot.GetComponentsInChildren<Transform>(true);
        foreach (var obj in all)
        {
            if (obj.name == name)
                return obj.gameObject;
        }
        return null;
    }
}
