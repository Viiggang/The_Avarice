using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSelect;
public enum Player_Type//enum클래스 바깥쪽 뺌
{
    Paladin,
    WindBreaker,
    Ignis,
    SoulEater,
    NULL=9999 //추가
};
public class PlayerMgr : BaseMgr<PlayerMgr>
{
 
    [Header ("-Player_Select")]
    [SerializeField]
    Player_Type playerType;

    public GameObject[] Player;

    [Space]
    [Header("Player_Info")]
    public int Plyer_Atk = 5;
    public float plyer_Speed = 5;

    
    
    public void Start()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].SetActive(false);
        }
        SetPlayerType();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player[(int)playerType].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < Player.Length; i++)
            {
                Player[i].SetActive(false);
            }
        }
    }

    void SetPlayerType()//추가
    {
        if(CharacterSelector.Instance.player_Type == Player_Type.NULL)
        {
            Debug.Log("문제 발생");
        }
        else
        {
            Debug.Log("정상작동");
            Debug.Log($"선택 캐릭터 이름:{CharacterSelector.Instance.player_Type}");
            playerType = CharacterSelector.Instance.player_Type;
        }
      
    }
}
