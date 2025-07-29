using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss_Colossal
{
    public class BossAblity : MonoBehaviour
    {
        private const int MaxHp = 1000;

        [Leein.InspectorName("이름")][SerializeField]private string Name;

        [Leein.InspectorName("보스 체력")][SerializeField] public int Hp;
        [Leein.InspectorName("보스 공격력")][SerializeField] public int Damage;


        private void InitAblity()
        {
            Name = "Colossal";
            Hp = MaxHp;
            Damage = 15;
        }
        private void Start()
        {
            InitAblity();
        }
    }

}

