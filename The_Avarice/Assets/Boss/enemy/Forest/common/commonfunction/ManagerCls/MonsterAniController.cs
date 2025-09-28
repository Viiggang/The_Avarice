using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAniController : MonoBehaviour
{
    [SerializeField] public List<MonsterAniData> MonsterAniList;
    [SerializeField] public Animator animator;
    private Dictionary<string, MonsterAniData> aniDict;
    private void Awake()
    {
        aniDict = MonsterAniList.ToDictionary(Data => Data.Playname, Data => Data);
        
    }

    public void Play(string PlayName)
    {
        aniDict[PlayName].Play(animator);
    }
}
