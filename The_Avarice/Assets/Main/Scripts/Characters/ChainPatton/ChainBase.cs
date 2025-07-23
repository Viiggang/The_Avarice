using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChainBase  
{
    protected ChainBase nextHandler;

    // �ļ� �ڵ鷯 ����
    public void SetNextHandler(ChainBase nextHandler)
    {
        this.nextHandler = nextHandler;
    }

    // ��û ó�� �޼ҵ�
    public abstract void HandleRequest(String _Name);
}
