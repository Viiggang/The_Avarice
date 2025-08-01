using Boss_Colossal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    public float radius = 5f;

    [Leein.InspectorName("찾은 콜라이더")] public Collider2D colliders;
    [Leein.InspectorName("찾을 레이어")][SerializeField] private LayerMask player;
    [SerializeField] public Vector3 pos;
    [SerializeField] public Vector3 size;
    void OnDrawGizmos()
    {
        //// 구체 시각화 (중심 위치와 반지름)
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
        Debug.Log("플레이어 접근 상태 전환");

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
            Debug.Log("플레이어 Layer가 있는지 체크");
        }
    }
}
