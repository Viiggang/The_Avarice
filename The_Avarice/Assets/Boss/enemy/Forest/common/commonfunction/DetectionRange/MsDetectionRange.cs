using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MsDetectionRange : MonoBehaviour
{
    [SerializeField]public Collider2D Collider;
    [SerializeField]private Vector3 size;
    [SerializeField]private Vector3 offset;
    [SerializeField]public SpriteRenderer renderer;
    [SerializeField]private LayerMask playerLayer;
    [SerializeField]public Collider2D findcollider;

    #region
    private Vector3 detectionCenter;
    private Vector3 center;
    private Vector2 scaledSize;
    private Vector3 scaledRight;
    #endregion
    [SerializeField]public Transform MonsterTrans;
    public bool gizmos=false;

    public const   float DlayTime = 1f;
    public float time = 0f;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
         Gizmos.color = UnityEngine.Color.red;
        if (Collider == null) return;
        if (renderer == null) return;

        center = Collider.bounds.center;
        
         scaledSize = new Vector2(
        size.x * transform.lossyScale.x,
        size.y * transform.lossyScale.y
         );
        
        // 로컬 오프셋 right에 스케일 반영
         scaledRight = new Vector3(
            offset.x * transform.lossyScale.x,
            offset.y * transform.lossyScale.y,
            offset.z * transform.lossyScale.z
        );

        // detectionCenter 계산 (월드 좌표)
        detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);

        Gizmos.DrawWireCube(detectionCenter, scaledSize);
    
    }
#endif
    private void Start()
    {
         findcollider = null;
         gizmos = false;
         scaledSize = new Vector2(
         size.x * transform.lossyScale.x,
         size.y * transform.lossyScale.y
         );

        // 로컬 오프셋 right에 스케일 반영
        scaledRight = new Vector3(
           offset.x * transform.lossyScale.x,
           offset.y * transform.lossyScale.y,
           offset.z * transform.lossyScale.z
            );
    }

    private void Update()
    {
      bool onCheck = time >= DlayTime;
      if (onCheck)
      {
            //콜라이터 사이즈 추출
            center = Collider.bounds.center;

            //스프라이트 FlipX 상태를 확인 후 탐색범위 보정한다. 
            detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);

            // 지정한 위치와 범위에 플레이어 레이어가 있는지 탐색한다.
            var hit = Physics2D.OverlapBox(detectionCenter, scaledSize, 0f, playerLayer);
            if (hit == null)
            {
                findcollider = null;
                return;
            }
            time = 0f;
            findcollider = hit;
      }
       else
        {
            time += Time.deltaTime;
        }
     
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(MsDetectionRange))]
public class MsDetectionRangeEditor : Editor
{
    private bool clicked = false; // 버튼 클릭 상태 저장
    
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 그리기
        DrawDefaultInspector();

        MsDetectionRange obj = (MsDetectionRange)target;
        string buttonText = obj.gizmos ? "인지 범위 활성화 상태" : "인지 범위 비활성화 상태";
        // 버튼 추가
        if (GUILayout.Button(buttonText))
        {
          
            obj.gizmos = !clicked;
            clicked = !clicked; // 클릭 후 상태 변경
            SceneView.RepaintAll();
        }
      
    }

}
#endif