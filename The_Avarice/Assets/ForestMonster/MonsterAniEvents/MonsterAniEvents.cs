using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAniEvents :MonoBehaviour
{
    public List<BaseAniEvent> AniEventList;
    public GameObject bulletPrefab;
    public List<GameObject> bulletPool;
    public MonsterController Controller;

    public Dictionary<string, BaseAniEvent> DicAniEvents;

    public int PoolSize;
    private void Start()
    {
        DicAniEvents = AniEventList.ToDictionary(ListData => ListData.trigger, ListData => ListData);
        if (bulletPrefab == null) return;
        for (int i = 0; i < PoolSize; i++)
        {

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public void Excute(string trigger)
    {
        DicAniEvents[trigger].Execute();
    }

    public void ArcherAttackExeCute(string trigger)//ÇØ°ñ±Ã¼ö
    {
        DicAniEvents[trigger].Execute(bulletPool);
    }
    public void MonsterNextStateExecute(string trigger)
    {
        DicAniEvents[trigger].Execute(Controller);
    }
}
