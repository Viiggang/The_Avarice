using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_BuffAtk : MonoBehaviour
{
    float AtkDamage = 5f;
    private void OnEnable()
    {
        StartCoroutine(offRange());
    }

    IEnumerator offRange()
    {
        yield return new WaitForSeconds(0.03f);
        this.gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>();
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            damage.OnHitDamage(AtkDamage);

        }

    }
}
