using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class Skill_Info : ScriptableObject
{
    enum Skill_Type
    {
        Active,
        Passive
    };
    enum Skilll_input
    {
        Down,
        Charge,
        Hold,
        None
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
    public float value;
    [Space]
    public string Skill_Trigger;//애니메이션 재생용
    public Sprite icon;
    //[Space]
   /* [SerializeReference] 
    public List<ISkillModule> activeModules;
    [SerializeReference] 
    public List<IPassiveModule> passiveModules;*/

}
