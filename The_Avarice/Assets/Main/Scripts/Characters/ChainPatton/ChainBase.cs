using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChainBase  
{
    protected ChainBase nextHandler;

    // 후속 핸들러 설정
    public void SetNextHandler(ChainBase nextHandler)
    {
        this.nextHandler = nextHandler;
    }

    // 요청 처리 메소드
    public abstract void HandleRequest(String _Name);
}
