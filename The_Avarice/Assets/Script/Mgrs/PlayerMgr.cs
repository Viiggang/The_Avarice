using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : BaseMgr<PlayerMgr>
{
    enum Player_Type
    {
        Paladin,
        WindBreaker,
        Ignis,
        SoulEater
    };
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
}
