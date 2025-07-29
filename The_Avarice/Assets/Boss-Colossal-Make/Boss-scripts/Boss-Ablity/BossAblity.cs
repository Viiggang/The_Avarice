using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss_Colossal
{
    public class BossAblity : MonoBehaviour
    {
        private const int MaxHp = 1000;

        [Leein.InspectorName("�̸�")][SerializeField]private string Name;

        [Leein.InspectorName("���� ü��")][SerializeField] public int Hp;
        [Leein.InspectorName("���� ���ݷ�")][SerializeField] public int Damage;


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

