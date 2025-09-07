using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hpbar : MonoBehaviour
{
    public RectTransform hpRect;
    public float maxWidth; // HP °¡µæ Ã¡À» ¶§ÀÇ ³Êºñ
    public float Hp =0;
    private float maxHp=0;
    private void Start()
    {
        maxWidth = 188f;
        SetHpData();
    }
    public void SetHP(float currentHP, float maxHP)
    {
        float width = (currentHP / maxHP) * maxWidth;
        hpRect.sizeDelta = new Vector2(width, hpRect.sizeDelta.y);
    }
    private void Update()
    {
        if (Hp == 0 && maxHp == 0) return;
        SetHP(Hp, maxHp);
    }
    private void OnDrawGizmos()
    {
        //if(Hp ==0&&maxHp ==0)return;
        // SetHP(Hp, maxHp);
    }
    private void  SetHpData()
    {
         
            maxHp = ColossalHandler.Instance.ablity.MaxHp;
            Hp = maxHp;
            
         
       
    }
}
