using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAtk : MonoBehaviour
{
    float AtkDamage = 1f;

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

        IDamage damage = other.GetComponent<IDamage>(); //충돌한 오브젝트에서 IDamage 인터페이스를 가져옮
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 충돌한 오브젝트가 IDamage인터페이스를 가지고있고 레이어가 enemy이라면
        {
            damage.OnHitDamage(AtkDamage); //인터페이스에 선언된 OnHitDamage()메소드를 호출 = 피격처리
            if (PlayerMgr.instance.playerType == Player_Type.Paladin && !PlayerMgr.instance.OnPassive)
            {
                PlayerMgr.instance.sumPassiveStack(1);
            }
        }
     
    }
}
