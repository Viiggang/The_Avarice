using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed; // ÃÑ¾Ë ¼Óµµ
   
    public Vector3 dir;
    public MonsterData ArcherDamage;
    float angle;
    public Vector3 size;
    public LayerMask player;
    public Vector3 hitsize;

    public SpriteRenderer monsterSprite;
    
    private void Awake() 
    {
        hitsize = GetComponent<BoxCollider2D>().bounds.size;
    }
    private void OnEnable()
    {
        if (monsterSprite == null)
            return;

        dir = monsterSprite.flipX ? new Vector3(-1f, 0, 0) : new Vector3(1f, 0, 0);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Invoke("Inactive", 5f);

    }
    private void Inactive()
    {
        this.gameObject.SetActive(false);
    }

    
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        var hitdata=Physics2D.OverlapBox(this.transform.position, hitsize,this.transform.rotation.z,player);
        if(hitdata !=null)
        {
            hitdata.GetComponent<IDamage>().OnHitDamage(ArcherDamage.Damage);
            this.gameObject.SetActive(false);
        }
      
    }
    
}
