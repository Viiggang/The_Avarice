using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : Singleton<DetectionRange>
{
    [Leein.InspectorName("찾은 콜라이더")] public Collider2D PlayerCollider;

    private Vector3 boxSize;
    private Vector3 center;
    private ColliderManager Manager;
    private Vector3 GizmosCenter;
    private Vector3 GizmosSize;

    public Vector3 size;
    public Vector3 Offset;
    public bool ShowGizmos=false;
    public LayerMask Player;
    private void Start()
    {
        Manager = ColossalHandler.Instance.colliderManager;
        center = Manager.HitBox.bounds.center;
        ShowGizmos = true;
    }
    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;
        Gizmos.color = Color.red;
        boxSize = Manager.HitBox.bounds.size;
        center = Manager.HitBox.bounds.center;

        GizmosCenter = center + Offset;
        GizmosSize = boxSize + size;
        Gizmos.DrawWireCube(GizmosCenter, GizmosSize);
        CreateRange();
    }
    public void SetRecognitionRange()
    {
        if(ColossalHandler.Instance.currentStage ==Stage.Stage1)
        {
            SetRange1();
        }
        else
        {
            SetRange2();
        }
    }
    public void CreateRange()
    {
        var hit=Physics2D.OverlapBox(GizmosCenter, GizmosSize,0f, Player);
        if(hit !=null)
        {
            PlayerCollider = hit;
            ColossalHandler.Instance.Near = true;
        }
        else
        {
            ColossalHandler.Instance.Near = false;
        }
    }
    private void SetRange1()
    {
        
    }
    private void SetRange2()
    {
        size.x = 0.46f;
        Offset.x = 0.21f;
    }
}
