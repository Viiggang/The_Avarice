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

    #region 1초마다 검사할때 사용할 데이터
    public const float DelayTime = 1f;
    public float time;
    #endregion

    #region 탐색할 때 사용되는 변수
    public BossController Controller;
    public Collider2D[] Colluder2Ds = new Collider2D[1];
    int Count=0;
    #endregion
    public void Update()
    {
        bool canDetect = time >= DelayTime;
        if (canDetect)
        {
            DetectPlayer();
        }
        else
        {
            time += Time.deltaTime;
        }
    }
    private void DetectPlayer()
    {
        Count = Physics2D.OverlapBoxNonAlloc(
            offset,
            size,
            angle,
            Colluder2Ds,
            playerLayer
        );

        if (Count > 0)
        {
            Controller.TargetPos = Colluder2Ds[0].transform;
            Destroy(GetComponent<DetectionRange>());
        }
    }
}
