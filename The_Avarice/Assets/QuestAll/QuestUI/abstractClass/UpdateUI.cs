using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UpdateUI :ScriptableObject
{
    //일단 단일 인자값 전달용이지만 추후에 어떤것들을 넘길지 몰라 params object []로  선언
    public abstract void init(object data,params object[] objects);
     public abstract void Update();//<--업뎃
  

}
