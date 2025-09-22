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

    private Vector3 detectionCenter;
    private Vector3 center;
    private Vector2 scaledSize;
    private Vector3 scaledRight;
   [SerializeField]public Transform MonsterTrans;
    [HideInInspector]public bool gizmos=false;
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
        
        // ���� ������ right�� ������ �ݿ�
         scaledRight = new Vector3(
            offset.x * transform.lossyScale.x,
            offset.y * transform.lossyScale.y,
            offset.z * transform.lossyScale.z
        );

        // detectionCenter ��� (���� ��ǥ)
        detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);

        Gizmos.DrawWireCube(detectionCenter, scaledSize);
        //var hit = Physics2D.OverlapBox(detectionCenter, scaledSize, 0f,playerLayer);
        //if (hit == null)
        //{
        //    findcollider = null;
        //    return;
        //}
        //findcollider = hit;
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

        // ���� ������ right�� ������ �ݿ�
        scaledRight = new Vector3(
           offset.x * transform.lossyScale.x,
           offset.y * transform.lossyScale.y,
           offset.z * transform.lossyScale.z
            );
    }
    private void Update()
    {
        if (findcollider != null) return;
        center = Collider.bounds.center;
        detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);
        var hit   = Physics2D.OverlapBox(detectionCenter, scaledSize, 0f, playerLayer);
        if (hit == null)
        {
            findcollider = null;
            return;
        }
        findcollider = hit;
    }

}
[CustomEditor(typeof(MsDetectionRange))]
public class MsDetectionRangeEditor : Editor
{
    private bool clicked = false; // ��ư Ŭ�� ���� ����
    
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �׸���
        DrawDefaultInspector();

        MsDetectionRange obj = (MsDetectionRange)target;
        string buttonText = obj.gizmos ? "���� ���� Ȱ��ȭ ����" : "���� ���� ��Ȱ��ȭ ����";
        // ��ư �߰�
        if (GUILayout.Button(buttonText))
        {
          
            obj.gizmos = !clicked;
            clicked = !clicked; // Ŭ�� �� ���� ����
            SceneView.RepaintAll();
        }
      
    }

}
