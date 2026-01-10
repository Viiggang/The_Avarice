using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawnManager : MonoBehaviour
{
    //생성할 몬스터  저장
    List<GameObject>monsterList=new List<GameObject>();
       
    [SerializeField]
    private float SpawnDelayTime; //스폰 딜레이 시간

    //문제 1 몬스터 스폰 지점에서 항상 동일한 몬스터가 나올 것인가?

    private void Start()=> StartCoroutine(SpawnMonster());

   
    public IEnumerator SpawnMonster()//1차
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelayTime);
            foreach (var monster in monsterList)
            {
                bool OnAtiveMonster = monster.activeSelf;
                if(!OnAtiveMonster)
                {
                    monster.SetActive(true);
                }
            }
        }
       
    }
}
