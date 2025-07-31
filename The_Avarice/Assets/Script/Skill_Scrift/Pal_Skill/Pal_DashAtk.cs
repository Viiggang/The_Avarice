using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_DashAtk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>(); //�浹�� ������Ʈ���� IDamage �������̽��� ������

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // �浹�� ������Ʈ�� IDamage�������̽��� �������ְ� ���̾ enemy�̶��
        {
            damage.OnHitDamage(2f); //�������̽��� ����� OnHitDamage()�޼ҵ带 ȣ�� = �ǰ�ó��
        }

    }
}
