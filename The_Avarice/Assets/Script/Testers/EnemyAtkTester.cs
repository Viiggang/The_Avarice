using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkTester : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>(); //충돌한 오브젝트에서 IDamage 인터페이스를 가져옮

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Player")) // 충돌한 오브젝트가 IDamage인터페이스를 가지고있고 레이어가 player이라면
        {
            damage.OnHitDamage(1f); //인터페이스에 선언된 OnHitDamage()메소드를 호출 = 피격처리
            Debug.Log("hurt2");
        }

    }
}
