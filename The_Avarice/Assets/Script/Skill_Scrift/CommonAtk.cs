using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAtk : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(offRange());
    }

    IEnumerator offRange()
    {
        yield return new WaitForSeconds(0.05f);
        this.gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>(); //�浹�� ������Ʈ���� IDamage �������̽��� ������

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // �浹�� ������Ʈ�� IDamage�������̽��� �������ְ� ���̾ enemy�̶��
        {
            damage.OnHitDamage(1f); //�������̽��� ����� OnHitDamage()�޼ҵ带 ȣ�� = �ǰ�ó��
        }

    }
}
