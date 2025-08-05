using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss_Colossal
{
    public class BossAblity : MonoBehaviour
    {
        public readonly float MaxHp = 1000f;

        [Leein.InspectorName("�̸�")][SerializeField]private string Name;

        [Leein.InspectorName("���� ü��")][SerializeField] public float Hp;
        [Leein.InspectorName("���� ���ݷ�")][SerializeField] public float Damage;


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

