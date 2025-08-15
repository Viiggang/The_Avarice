using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherAni : MonoBehaviour
{
    [SerializeField]private List<MonserAniData> MonsterAniList;
    [SerializeField] private Animator animator;
    public Dictionary<string, MonserAniData> Ani;
    private void Awake()
    {
        Ani = MonsterAniList.ToDictionary(Data => Data.Playname, Data => Data);
    }
  
    public void Play(string PlayName)
    {
        Ani[PlayName].Play(animator);
    }
    
}
