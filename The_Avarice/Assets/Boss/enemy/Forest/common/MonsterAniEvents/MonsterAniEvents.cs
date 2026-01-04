using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BulletPoolManager//원거리
{
    public GameObject bulletPrefab;
    public BulletPos bulletPos;
    public List<GameObject> bulletPool = new List<GameObject>();
    public int poolSize;
    public SpriteRenderer monsterSprite;

    public void InitializePool()
    {
        if (bulletPrefab == null) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().monsterSprite = monsterSprite;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public void ClearPool()
    {
        foreach (GameObject bullet in bulletPool)
        {
            GameObject.Destroy(bullet);
        }
        bulletPool.Clear();
    }
}
[System.Serializable]
public class  AttackCollisionData//근거리
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rigid;
    public Vector3 offset;
    public Vector3 size;
    public LayerMask playerLayer;

    public Vector3 GetGizmoCenter()
    {
        return boxCollider.bounds.center + offset;
    }
}
public class MonsterAniEvents : MonoBehaviour
{
    #region
    public GameObject DestroyObj;
    public MonsterController Controller;
    #endregion

    #region
    public List<BaseAniEvent> aniEventList;
    public Dictionary<string, BaseAniEvent> dicAniEvents;
    #endregion

    #region
    [SerializeField] public BulletPoolManager bulletPoolManager = new BulletPoolManager();
    [SerializeField] public AttackCollisionData attackCollisionData = new AttackCollisionData();
    #endregion

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (attackCollisionData.boxCollider == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackCollisionData.GetGizmoCenter(), attackCollisionData.size);
    }

#endif
    public void Start() => Init();


    public void Init()
    {
        // 수정
        dicAniEvents = aniEventList.ToDictionary(data => data.trigger, data =>Instantiate(data));

        bool hasBullet = (bulletPoolManager.bulletPrefab != null);
        if (hasBullet) 
                bulletPoolManager.InitializePool();
    }

    public void MonsterDeadEvent()
    {
        Debug.Log("몬스터 죽음 삭제 실행");
        bulletPoolManager?.ClearPool();
        string deah = "death";
        dicAniEvents[deah].Execute( DestroyObj);
       /// Destroy(DestroyObj);
    }
    /*
     수정해야할 사항들
     근거리 원거리 대쉬 공격 기타등등 공격 메소드 명확하게 작성하기
    
     */
  

    public void ArcherAttackExeCute(string trigger)=> dicAniEvents[trigger].Execute(bulletPoolManager.bulletPool, bulletPoolManager.bulletPos);


    public void MonsterNextStateExecute(string trigger) => dicAniEvents[trigger].Execute(Controller);

    public void SkeletonAxemanAttack(string trigger)
    {
        dicAniEvents[trigger].Execute(
            GetComponent<SpriteRenderer>(),
            attackCollisionData.boxCollider,
            ref attackCollisionData.offset,
            ref attackCollisionData.size,
            attackCollisionData.playerLayer);
    }

    public void ElectricAttack(string trigger)
    {
        dicAniEvents[trigger].ElectricAttack(
            this.transform.position+ attackCollisionData.offset,
           attackCollisionData.size,
            attackCollisionData.playerLayer);
    }

    public void DashAttack(string trigger)=> dicAniEvents[trigger].Execute(Controller, attackCollisionData.rigid);
   
}

 