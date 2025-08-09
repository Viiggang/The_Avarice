using Colossal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public BoxCollider2D footCollider;
    public BoxCollider2D HitBox;

    private Dictionary<string, ICollider> colliders;

    [System.Serializable]
    public struct SizeAndOffset
    {
        public Vector2 size;
        public Vector2 offset;
    }
    public SizeAndOffset footColliderData;
    public SizeAndOffset hitBoxData;

   
    private void Awake()
    {
        InitDictionary();

    }
    void Start()
    {
        setCollideSizeAndOffset();
        
    }
    private void setCollideSizeAndOffset()//콜라이더 사이즈와 간격 설정 값들은 인스펙트창에서 보면서 조정
    {
        colliders["Foot"].SetColliderSizeAndOffset(footColliderData.size, footColliderData.offset);
        colliders["HitBox"].SetColliderSizeAndOffset(hitBoxData.size, hitBoxData.offset);
    }
    private void InitDictionary()
    {
        colliders = new Dictionary<string, ICollider>()
        {
               { "Foot", new Foot(footCollider) },
               { "HitBox", new HitBox(HitBox) }
        };
    }

}
