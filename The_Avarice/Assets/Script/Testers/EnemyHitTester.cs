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
        Debug.Log("Enemy_hurt1 {0}" + damage);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("[Hitbox Trigger]: {0}", other.gameObject.name);
    }
    IEnumerator OnHit()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        mat.color = Color.white;
    }
}
