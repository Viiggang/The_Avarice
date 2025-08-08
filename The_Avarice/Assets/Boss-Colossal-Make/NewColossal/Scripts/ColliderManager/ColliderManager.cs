using Colossal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public BoxCollider2D footCollider;
    public BoxCollider2D DetectionZone;
    public BoxCollider2D HitBox;

    private Dictionary<string, ICollider> colliders;

    [System.Serializable]
    public struct SizeAndOffset
    {
        public Vector2 size;
        public Vector2 offset;
    }
    public SizeAndOffset footColliderData;
    public SizeAndOffset detectionZoneData;
    public SizeAndOffset hitBoxData;

   
   
    private void setCollideSizeAndOffset()
    {
        colliders["Foot"].SetColliderSizeAndOffset(footColliderData.size, footColliderData.offset);
        colliders["DetectionZone"].SetColliderSizeAndOffset(detectionZoneData.size, detectionZoneData.offset);
        colliders["HitBox"].SetColliderSizeAndOffset(hitBoxData.size, hitBoxData.offset);
    }
    private void Awake()
    {
        colliders = new Dictionary<string, ICollider>()
        {
               { "Foot", new Foot(footCollider) },
               { "DetectionZone", new DetectionZone(DetectionZone) },
               { "HitBox", new HitBox(HitBox) }
        };

    }
    void Start()
    {
        setCollideSizeAndOffset();
        
    }

    
}
