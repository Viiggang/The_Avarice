using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAniController : MonoBehaviour
{
    [SerializeField] public List<MonsterAniData> MonsterAniList;//사용할 애니메이션 트리거 네임 데이터  저장
    private List<MonsterAniData> RunTimeMonsterAniList=new List<MonsterAniData>();//런 타임 때  쓸 List
    [SerializeField] public Animator animator;//현재 해당 게임오브젝트에 붙어 있는 애니메이션 기록
    private Dictionary<string, MonsterAniData> aniDict;// 빠르게 접근하려고 Dictionary 사용

     
    private void Awake()
    {
        RunTimeMonsterAniList = MonsterAniList.Select(Data => Instantiate(Data)).ToList();// 저장한 데이터를 복사 하여 런타임 변수에 삽입
        aniDict = RunTimeMonsterAniList.ToDictionary(Data => Data.Playname, Data => Data);//딕셔너리로 치환
        
    }

    public void Play(string PlayName)
    {
        
          aniDict[PlayName].Play(animator);
    }
    /*
    애니메이션 출력을 확장성,간편하게 사용할 수 있게 하려고 이렇게 설계함
    
    현재 인스펙트 창에서 스크립트 오브젝트를 List<T>에 넣어서 쉽게 추가할수 있게 해놓고
     Play(string PlayName) 함수를 사용해서  해당 애니메이션을 출력한다.
     */
}
