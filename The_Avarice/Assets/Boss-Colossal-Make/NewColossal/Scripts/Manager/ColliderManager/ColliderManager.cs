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
    private void setCollideSizeAndOffset()//�ݶ��̴� ������� ���� ���� ������ �ν���Ʈâ���� ���鼭 ����
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
