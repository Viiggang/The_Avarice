using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hpbar : MonoBehaviour
{
    public RectTransform hpRect;
    public float maxWidth = 188f; // HP °¡µæ Ã¡À» ¶§ÀÇ ³Êºñ
    public float Hp = 200;
    private float maxHp = 200;
    public void SetHP(float currentHP, float maxHP)
    {
        float width = (currentHP / maxHP) * maxWidth;
        hpRect.sizeDelta = new Vector2(width, hpRect.sizeDelta.y);
    }
    private void OnDrawGizmos()
    {
        SetHP(Hp, maxHp);
    }

}
