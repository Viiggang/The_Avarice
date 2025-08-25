using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ArcherAniEvents : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Delete;
    public MonsterController controller;
    public List<GameObject> bulletPool;
    public SpriteRenderer spriteRenderer;
    public BulletPos pos;
    public int PoolSize = 0;
    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        bulletPool = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); 
            bulletPool.Add(bullet);
        }
    }
    private void Update()
    {
        
    }
    public void Dead()
    {
        Debug.Log("»èÁ¦");
        foreach (GameObject bullet in bulletPool)
        {
            Destroy(bullet);
        }
        Destroy(Delete);
    }
    public void Attacktoidle()
    {
        Debug.Log("Attacktoidle");
        controller.MonsterMachine.ChangeState(controller.State["idle"], controller);
    }
    public void Attack()
    {
        pos.SetBulletPos();
        var bullet = GetPooledBullet();
      
        bullet.transform.position = pos.transform.position; 
        bullet.transform.rotation= Quaternion.identity;
        bullet.SetActive(true);
    }
    private GameObject GetPooledBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if(!bullet.activeSelf)
            {
                return bullet;
            }
        }
        //if (bulletPool.Count==1)
        //{
        //    for (int i = 0; i < PoolSize; i++)
        //    {
        //        GameObject obj = Instantiate(bulletPrefab);
        //        obj.SetActive(false);
        //        bulletPool.Add(obj);
        //    }
        //}
        //foreach (GameObject bullet in bulletPool)
        // {

        //    if (!bullet.activeInHierarchy)
        //    {
        //        bulletPool.RemoveAt(0);
        //        return bullet;
        //    }
        //}
        return null;
    }
}
