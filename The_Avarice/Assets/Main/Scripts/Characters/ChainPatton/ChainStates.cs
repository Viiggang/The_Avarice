using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ChainBase
{
    public override void HandleRequest(string _Name)
    {
        if (_Name == "��������")//<--Ʈ�� �� ���� �б�
        {
            Debug.Log("��������");  //<--����
        }
        else//�ƴϸ� ���� �ڵ�� �ѱ�
        {
            nextHandler.HandleRequest(_Name); // ���� �ڵ鷯�� ��û ����
        }
    }
}

public class NPC : ChainBase
{
    public override void HandleRequest(string _Name)
    {
        if (_Name == "NPC")//<--Ʈ�� �� ���� �б�
        {
            Debug.Log("NPC");  //<--����
            
        }
        else//�ƴϸ� ���� �ڵ�� �ѱ�
        {
            nextHandler.HandleRequest(_Name); // ���� �ڵ鷯�� ��û ����
        }
    }
}

