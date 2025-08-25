using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed; // 총알 속도
    public Transform target;
    public Vector3 dir;
    public MonsterData ArcherDamage;
    float angle;
    public Vector3 size;
    public LayerMask player;
    public Vector3 hitsize;
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(this.transform.position, size);
      
    }
    private void Awake()//플레이어 찾기
    {
        hitsize = GetComponent<BoxCollider2D>().bounds.size;
    }
    void Start()
    {
        var hit = Physics2D.OverlapBox(this.transform.position, size, 0f, player);
        if (hit != null)
        {
            target = hit.GetComponent<Transform>();
        }
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        dir = (target.position - transform.position).normalized;
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
