using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
public enum Player_Type
{
    Paladin,
    WindBreaker,
    Ignis,
    SoulEater,
    NULL = 9999
};

public class PlayerMgr : BaseMgr<PlayerMgr>
{

    [Header("-Player_Select")]

    Player_Type playerType;

    public GameObject[] Player;

    [Space]
    [Header("Player_Info")]
    [SerializeField]
    private float Player_MaxHp = 100f;
    [SerializeField]
    private float Player_Hp = 100f;
    [SerializeField]
    private float Player_Attack = 5f;
    [SerializeField]
    private float Player_addedAtk = 0f;
    [SerializeField]
    private float plyer_Speed = 5f;
    [SerializeField]
    private int player_passive1 = 0;
    public Vector2 Startpos;

    private bool Pal_PassiveSward = false;
    private bool Pal_PassiveShield = false;


    public void Start()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].SetActive(false);
        }
    }

    public void SelectSponplayer()
    {
        this.gameObject.transform.position = Startpos;
        Player[(int)playerType].SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectSponplayer();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < Player.Length; i++)
            {
                Player[i].SetActive(false);
            }
        }


        if (playerType == Player_Type.Paladin)
        {
            if (player_passive1 > 20)
            {
                player_passive1 = 20;
            }
        }

    }

    public void setStartPos(Vector2 pos)
    {
        Startpos = pos;
    }

    public Player_Type getPlayerType()
    {
        return playerType;
    }

    public void setPassiveStack(int stack)
    {
        player_passive1 += stack;
    }
    public int getPassiveStack()
    {
        return player_passive1;
    }

    public float getPlayerSpeed()
    {
        return plyer_Speed;
    }
    public void setPlayerSpeed(float sum)
    {
        plyer_Speed = sum;
    }

    public float getPlayerAtk()
    {
        return Player_Attack;
    }

    public void setPlayerAtk(float sum) //기초공격력 제어
    {
        Player_Attack += sum;
    }

    public void setPlayeraddedAtk(float sum)//추가 공격력 제어
    {
        Player_addedAtk += sum;
    }

    public float getPlayerHp()
    {
        return Player_Hp;
    }
    public void setPlayerHp(float sum)
    {
        Player_Hp += sum;
    }
    public float getPlayerMaxHp()
    {
        return Player_MaxHp;
    }

    public void setPlayerMaxHp(float sum)
    {
        Player_MaxHp += sum;
    }

    public bool getPassive()
    {
        if(playerType == Player_Type.Paladin)
        {
            return Pal_PassiveSward;
        }
        return false;
    }

    public void setPassive(bool set)
    {
        if(playerType == Player_Type.Paladin)
        {
            Pal_PassiveSward = set;
        }
    }
}