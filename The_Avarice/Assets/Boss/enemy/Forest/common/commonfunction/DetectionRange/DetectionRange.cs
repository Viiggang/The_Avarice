using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    #region physics2D OverlapBoxNonAlloc 용 데이터
    public LayerMask playerLayer;
    public Vector3 offset;
    public Vector2 size;
    public float angle = 0;
    #endregion
 

    #region 탐색할 때 사용되는 변수
    public BossController Controller;
    public Collider2D Collider2d = new Collider2D();
    int Count=0;
    #endregion
    public void Update() => DetectPlayer();

    private void DetectPlayer()
    {
       Collider2d = Physics2D.OverlapBox(this.transform.position, size, 0f, playerLayer);

        if (Collider2d != null)
        {
            Controller.TargetPos = Collider2d.transform;
            Destroy(GetComponent<DetectionRange>());
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube( this.transform.position+offset, size);
    }
#endif
}
