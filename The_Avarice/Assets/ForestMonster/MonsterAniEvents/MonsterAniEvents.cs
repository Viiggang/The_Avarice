using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAniEvents :MonoBehaviour
{
    public List<BaseAniEvent> AniEventList;
    public List<GameObject> bullets;
    public MonsterController Controller;

    public Dictionary<string, BaseAniEvent> DicAniEvents;

    private void Awake()
    {
        DicAniEvents = AniEventList.ToDictionary(ListData => ListData.trigger, ListData => ListData);
    }
    public void Excute(string trigger)
    {
        DicAniEvents[trigger].Execute();
    }

    public void ArcherAttackExeCute(string trigger)//ÇØ°ñ±Ã¼ö
    {
        DicAniEvents[trigger].Execute(bullets);
    }
    public void MonsterNextStateExecute(string trigger)
    {
        DicAniEvents[trigger].Execute(Controller);
    }
}
