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

        IDamage damage = other.GetComponent<IDamage>(); //�浹�� ������Ʈ���� IDamage �������̽��� ������
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // �浹�� ������Ʈ�� IDamage�������̽��� �������ְ� ���̾ enemy�̶��
        {
            damage.OnHitDamage(AtkDamage); //�������̽��� ����� OnHitDamage()�޼ҵ带 ȣ�� = �ǰ�ó��
            if (PlayerMgr.instance.playerType == Player_Type.Paladin && !PlayerMgr.instance.OnPassive)
            {
                PlayerMgr.instance.sumPassiveStack(1);
            }
        }
     
    }
}
