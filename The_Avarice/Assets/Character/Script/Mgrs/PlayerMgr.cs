using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public enum Player_Type
{
    Paladin,
    Ignis,
    WindBreaker,
    SoulEater,
    NULL = 9999
};

public enum Element_Type
{
    Fire,
    Thunder,
    Ice,
    NULL = 9999
};

public class PlayerMgr : BaseMgr<PlayerMgr>
{
    public Player_Type playerType { get; private set; }

    public GameObject[] playerPrefab;
    private GameObject player;


    [Header("Player_Passive")]
    private int player_passive1 = 0;
    private int player_passive2 = 0;
    private Element_Type element_Type;

    private float Player_Guard = 1f;

    public GameObject Startpos;

    private bool onPassive2 = false;
    private bool onPassive = false;

    private int Pal_ShiledPassive = 0;

    private int Ign_trinityPassive = 0;
    private bool Ign_onPassive = false;

    [Header("- Player_Info")]
    [SerializeField] private float Player_MaxHp = 100f;
    [SerializeField] private float Player_Hp = 100f;
    [SerializeField,Tooltip("기초공격력")] private float Player_Attack = 5f;
    [SerializeField, Tooltip("추가공격력")] private float Player_addedAtk = 0f;
    [SerializeField, Tooltip("기초방어력")] private float Player_Defense = 5f;
    [SerializeField, Tooltip("추가방어력")] private float Player_addedDef = 0f;

    [Space ,Header("- Attack Anim Settings")]
    [Range(1f, 5f), Tooltip("공격 애니메이션 속도(공격속도)")]public float attackSpeed = 1.0f;
    [Range(1f, 5f), Tooltip("기본 애니메이션 속도")]public float nomal_Speed = 1.0f;

    [Space, Header("- Movement Settings")]
    [Range(2f, 10f)] public float Speed = 5f;
    [Range(5f, 20f)] public float jumpPower = 20f;

    [Space, Header("- Dash Settings")]
    [Range(20f, 50f)] public float dashSpeed = 30f;
    [Range(0.05f, 0.3f)] public float dashDuration = 0.1f;
    [Range(0.2f, 3f)] public float dashCooldown = 1f;
    [Range(0.02f, 0.15f)] public float dashDodge = 0.05f;
    [Range(0.1f, 3f)] public float skillaccelerate = 1f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float maxSlopeAngle = 45f;
    public float groundRayDistance = 0.05f;

    public bool Direction { get; set; } = true; // 바라보는 방향
    public float MaxHp
    {
        get => Player_MaxHp;
        set => Player_MaxHp = value;
    }

    public float Hp
    {
        get => Player_Hp;
        set => Player_Hp = value;
    }

    public float Attack
    {
        get => Player_Attack;
        set => Player_Attack = value;
    }

    public float AddedAtk
    {
        get => Player_addedAtk;
        set => Player_addedAtk = value;
    }

    public float Defense
    {
        get => Player_Defense;
        set => Player_Defense = value;
    }

    public float AddedDef
    {
        get => Player_addedDef;
        set => Player_addedDef = value;
    }

    public float Move_Speed
    {
        get => Speed;
        set => Speed = value;
    }

    public float JumpPower
    {
        get => jumpPower;
        set => jumpPower = value;
    }

    public float DashSpeed
    {
        get => dashSpeed;
        set => dashSpeed = value;
    }

    public float DashDuration
    {
        get => dashDuration;
        set => dashDuration = value;
    }

/*    public float Skill1_Duration
    {
        get => Skill1Duration;
        set => Skill1Duration = value;
    }
*/
    public float DashCooldown
    {
        get => dashCooldown;
        set => dashCooldown = value;
    }

/*    public float Skill1_Cooldown
    {
        get => skill1Cooldown;
        set => skill1Cooldown = value;
    }*/

/*    public float ResetCooldown
    {
        get => resetCooldown;
        set => resetCooldown = value;
    }*/

    public float SkillAccelerate
    {
        get => skillaccelerate;
        set => skillaccelerate = value;
    }

    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    public float Nomal_Speed
    {
        get => nomal_Speed;
        set => nomal_Speed = value;
    }

    public float DashDodge
    {
        get => dashDodge;
        set => dashDodge = value;
    }

    public int Passive1
    {
        get => player_passive1;
        private set => player_passive1 = value;
    }

    public int Passive2
    {
        get => player_passive2;
        private set => player_passive2 = value;
    }

    public Element_Type ElementType
    {
        get => element_Type;
        set => element_Type = value;
    }

    public bool Passive
    {
        get => playerType == Player_Type.Paladin && onPassive2;
        set { if (playerType == Player_Type.Paladin) onPassive2 = value; }
    }

    public bool OnPassive
    {
        get => onPassive;
        set => onPassive = value;
    }


    public int Ign_TrinityPassive
    {
        get => Ign_trinityPassive;
        set => Ign_trinityPassive = value;
    }

    public bool Ign_OnPassive
    {
        get => Ign_onPassive;
        set => Ign_onPassive = value;
    }


    [Tooltip("받는 최종데미지의 총량 1이면 100%로 온전한 최종데미지를 넣고 0.5면 최종데미지의 절반만받는다.")]
    public float Guard
    {
        get => Player_Guard;
        set => Player_Guard = value;
    }

    
    public void Spawnplayer()
    {
        this.gameObject.transform.position = Startpos.transform.position;
        player = Instantiate(playerPrefab[(int)playerType].gameObject, Startpos.transform.position, Quaternion.identity);
        CameraManager.Instance.SetTarget(player.transform);
    }

    public void Update()
    {
        if (playerType == Player_Type.Paladin && playerType == Player_Type.Ignis)
        {
            if (player_passive1 > 20)
            {
                player_passive1 = 20;
            }
        }
    }

    public void setStartPos(GameObject pos) => Startpos = pos;

    public void setPlayerType(Player_Type set) => playerType = set;

    public void sumPassiveStack(int stack)
    {
        player_passive1 += stack;
        Pal_ShiledPassive = 0;
    }

    public void sumPassive2Stack(int stack) => player_passive2 += stack;

    public void sumPlayerAtk(float sum) => Player_Attack += sum;

    public void sumPlayeraddedAtk(float sum) => Player_addedAtk += sum;

    public void sumPlayerHp(float sum) => Player_Hp += sum;
    public void sumPlayerHit(float sum) => Player_Hp -= sum * Guard;

    public void sumPlayerMaxHp(float sum) => Player_MaxHp += sum;

    public void setShieldPassive()
    {
        if (Pal_ShiledPassive < 10)
        {
            Pal_ShiledPassive += 2;
            player_passive1 += 2;
        }
    }
}