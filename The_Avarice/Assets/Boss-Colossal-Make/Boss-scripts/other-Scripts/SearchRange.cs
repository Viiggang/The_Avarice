using Boss_Colossal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    public float radius = 5f;

    [Leein.InspectorName("ã�� �ݶ��̴�")] public Collider2D colliders;
    [Leein.InspectorName("ã�� ���̾�")][SerializeField] private LayerMask player;
    [SerializeField] public Vector3 pos;
    [SerializeField] public Vector3 size;
    void OnDrawGizmos()
    {
        //// ��ü �ð�ȭ (�߽� ��ġ�� ������)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);
        Gizmos.DrawCube(this.transform.position + pos, size);
    }
    private void Start()
    {
        InitSerchLayer();

    }

   

    private void Update()
    {
        FindPlayer();

    }

    private void FindPlayer()
    {
        colliders = Physics2D.OverlapCircle(this.transform.position, radius, player);
        bool hasFoundPlayer = (colliders != null);
        if (hasFoundPlayer)
        {
            changeState();
        }
    }

    private void changeState()
    {
        Debug.Log("�÷��̾� ���� ���� ��ȯ");

        BossController.Instance.target = colliders.GetComponent<Transform>();
        BossController.Instance?.ChangeState(new WakeState());
      
       Destroy(this.gameObject);
    }
    private void InitSerchLayer()
    {

        int playerLayer = LayerMask.NameToLayer("Player");
        bool hasLayer = playerLayer != -1;
        if (hasLayer)
        {
            player = 1 << playerLayer;
        }
        else
        {
            Debug.Log("�÷��̾� Layer�� �ִ��� üũ");
        }
    }
}
