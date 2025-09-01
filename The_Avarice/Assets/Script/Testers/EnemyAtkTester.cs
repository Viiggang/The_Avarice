using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkTester : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>(); //�浹�� ������Ʈ���� IDamage �������̽��� ������

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Player")) // �浹�� ������Ʈ�� IDamage�������̽��� �������ְ� ���̾ player�̶��
        {
            damage.OnHitDamage(1f); //�������̽��� ����� OnHitDamage()�޼ҵ带 ȣ�� = �ǰ�ó��
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            gameObject.transform.position = new Vector2(2.69f, -2.8f);
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {

            gameObject.transform.position = new Vector2(-2.1818f, -4.0953f);
        }
    }
}
