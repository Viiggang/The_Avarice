using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    #region physics2D OverlapBoxNonAlloc 용 데이터
    public LayerMask playerLayer;
    public Vector2 offset;
    public Vector2 size;
    public float angle = 0;
    #endregion
 

    #region 탐색할 때 사용되는 변수
    public BossController Controller;
    public Collider2D Colluder2Ds = new Collider2D();
    int Count=0;
    #endregion
    public void Update() => DetectPlayer();

    private void DetectPlayer()
    {
       Colluder2Ds = Physics2D.OverlapBox(this.transform.position, size, 0f, playerLayer);

        if (Colluder2Ds != null)
        {
            Controller.TargetPos = Colluder2Ds.transform;
            Destroy(GetComponent<DetectionRange>());
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( this.transform.position, size);
    }
#endif
}
