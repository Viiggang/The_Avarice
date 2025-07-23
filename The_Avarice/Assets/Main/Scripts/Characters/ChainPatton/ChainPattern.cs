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
    플레이가 레이캐스트를 쏴서 
    RaycastHit2D hit=Physics2D.Raycast(transform.position, direction, distance);
    로 hit.콜라이더. name을 받는다
    이름을 가져오면 TypeName 으로 저장
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
    
    //지금 사용하는것은 아니고 이런식으로 쓰라고 임시 기록
    //HandleRequest 뭔가 애매하면 ref로 ray도 같이 보내서 사용
  
  
}
