using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectEvents : MonoBehaviour
{
    public Transform player;
    public Vector3 pos;
    private IDamage target;
    private bool flag=true;
    public GameObject bossobj;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position,(transform.position- pos));
    }
    public void death()
    {
        Destroy(bossobj); 
    }
    public void AwaketoIdle()
    {
        ElectricSphereMachine.Instance.SetNextState("output");
    }
    public void ElectAttack1()
    {
        flag = true;
        StartCoroutine(Attack1Loop());

    }
    public void ElectAttack2()
    {
        flag = true;
        StartCoroutine(Attack2Loop());

    }
    public void stopAttack1()
    {
        flag = false;
    }
    private IEnumerator Attack1Loop()
    {
        while (flag)
        {
            var Hit = Physics2D.Linecast(this.transform.position, (transform.position - pos));
            if (Hit.collider == null)
            {
                yield return null;
                continue;
            }

            target = Hit.collider.gameObject.GetComponent<IDamage>();
            if (target == null)
            {
                yield return null;
                continue;
            }
            //최대 체력 1%피해
            target.OnHitDamage(1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Attack2Loop()
    {
        while (flag)
        {
            var Hit = Physics2D.Linecast(this.transform.position, (transform.position - pos));
            if (Hit == null) continue;

            target = Hit.collider.gameObject.GetComponent<IDamage>();
            if (target == null) continue;
            
            //최대 체력 3%피해
            target.OnHitDamage(1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
