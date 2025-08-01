using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitTester : MonoBehaviour, IDamage
{
    Material mat;

    public void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }
    public void OnHitDamage(float damage) //피격 인터페이스
    {
        StartCoroutine(OnHit());
    }
    IEnumerator OnHit()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        mat.color = Color.white;
    }
}
