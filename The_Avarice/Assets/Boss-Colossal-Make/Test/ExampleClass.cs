using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Boss_Colossal
{
    public class ExampleClass : MonoBehaviour
    {
        public float radius = 5f;

        public Collider2D[] colliders;
        [SerializeField] private LayerMask player;
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 size;
        void OnDrawGizmos()
        {
            //// ��ü �ð�ȭ (�߽� ��ġ�� ������)
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, radius);
            Gizmos.DrawCube(this.transform.position + pos, size);
        }
        private void Update()
        {


            colliders = Physics2D.OverlapCircleAll(this.transform.position, radius, player).Where(c => c.isTrigger == true).ToArray();
            if (colliders.Length > 0)
            {
                change();
            }

        }
        private void change()
        {
            Debug.Log("�÷��̾� ���� ���� ��ȯ");

            BossController.instance.target = colliders[0].GetComponent<Transform>();
            BossController.instance?.ChangeState(new WakeState());
            var item = GetComponent<ExampleClass>();
            Destroy(this.gameObject);
        }
    }
}

