using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ChainBase
{
    public override void HandleRequest(string _Name)
    {
        if (_Name == "보물상자")//<--트루 값 실행 분기
        {
            Debug.Log("보물상자");  //<--구현
        }
        else//아니면 다음 핸들로 넘김
        {
            nextHandler.HandleRequest(_Name); // 다음 핸들러로 요청 전달
        }
    }
}

public class NPC : ChainBase
{
    public override void HandleRequest(string _Name)
    {
        if (_Name == "NPC")//<--트루 값 실행 분기
        {
            Debug.Log("NPC");  //<--구현
            
        }
        else//아니면 다음 핸들로 넘김
        {
            nextHandler.HandleRequest(_Name); // 다음 핸들러로 요청 전달
        }
    }
}

