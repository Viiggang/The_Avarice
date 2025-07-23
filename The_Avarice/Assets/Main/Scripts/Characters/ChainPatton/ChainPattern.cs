using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class ChainPattern : MonoBehaviour
{
    Box ChainBox;
    NPC ChainNPC;
   string Name="NPC";//<test
   public string TypeName
    {
        get 
        {
            return Name;
        }
        set 
        {
            Name = value;
        }
    }

    /*
    �÷��̰� ����ĳ��Ʈ�� ���� 
    RaycastHit2D hit=Physics2D.Raycast(transform.position, direction, distance);
    �� hit.�ݶ��̴�. name�� �޴´�
    �̸��� �������� TypeName ���� ����
    */
    private void Awake()
    {
         ChainBox = new Box();
         ChainNPC = new NPC();
        ChainBox.SetNextHandler(ChainNPC);
    }
   public  void TypeSearch()
    {
        ChainBox.HandleRequest(TypeName);
    }
    
    //���� ����ϴ°��� �ƴϰ� �̷������� ����� �ӽ� ���
    //HandleRequest ���� �ָ��ϸ� ref�� ray�� ���� ������ ���
  
  
}
