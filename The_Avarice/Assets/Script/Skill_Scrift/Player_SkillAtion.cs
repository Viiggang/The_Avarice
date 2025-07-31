using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;
[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class Player_SkillAtion : ScriptableObject
{
    enum Skill_Type
    {
        Active,
        Passive
    };
    enum Skilll_input
    {
        Down,
        charge,
        none
    }

    [Header("- Skill_Info")]
    [SerializeField]
    private Skill_Type skillType;
    [SerializeField]
    private Skilll_input inputType;
    [Space]
    public float Damage;
    public float CoolDown;
    public float Duration;
    [Space]
    public string Skill_Trigger;
    public Sprite icon;


}
