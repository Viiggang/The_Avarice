using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Colossal
{
    public class UIManager : Singleton<UIManager>
    {
        //���� ���� ü�� �� Ǯ�� ü��
        public BossStatus BossHp; //<-- ���߿� ���� ü�� status�� ����
        public float MaxHp;

        //HpLine  �ִ� ����
        public float maxwidth;

        //HpUI
        public RectTransform HpLine;

        //������ 2 ���Խ� �۵� ��ų �Լ���
        public event Action OnPhase2Start;

        //Hp 0�̵Ǹ� ĵ���� ������ ����
        public GameObject DestroyCanvas;

        //Ȱ��ȭ ��ų ������Ʈ
        public GameObject hpbar;

        private bool hpUIShown = false;
        private void Awake()
        {
            hpbar.SetActive(false);
            base.Init();
            OnPhase2Start += ShowBossHealthUIIfHalf;
            OnPhase2Start += UpdateUI;
        }
        private void OnEnable()
        {
            //������ 2,ü�� ������ �Ǹ� UI �����µ� �̶� �ѹ� ����
            UpdateUI();
        }

        public void EventExecute()//����� �̺�Ʈ ó��
        {
            OnPhase2Start?.Invoke();
        }

        public void ShowBossHealthUIIfHalf()//UI�� �������� �Ǵ�.
        {
          
            bool Onable = BossHp.hp <= (MaxHp / 2);
            if (Onable && !hpUIShown)
            {
                hpbar.SetActive(true);
                hpUIShown = true;
                OnPhase2Start -= ShowBossHealthUIIfHalf;
            }
        }
        public void UpdateUI()//���� �ǰݴ��� ������ UI�������� ����
        {
        
            if (!hpUIShown) return;
            bool death = BossHp.hp <= 0;
            if (death)
            {
                Destroy(DestroyCanvas);
            }
            float width = (BossHp.hp /BossHp.MaxHp) * maxwidth;
            HpLine.sizeDelta = new Vector2(width, HpLine.sizeDelta.y);
        }


    }
#if UNITY_EDITOR
    [CustomEditor(typeof(UIManager))]
    public class MyComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();  // �⺻ �ν����� �׸���

            var myTarget = (UIManager)target;

            if (GUILayout.Button("UI  ������Ʈ"))
            {
                myTarget.UpdateUI();
            }
            if (GUILayout.Button("������Ʈ ������ 2 ����"))
            {
                myTarget.BossHp.hp = (myTarget.BossHp.MaxHp / 2);
                myTarget.EventExecute();
            }
            if (GUILayout.Button("�Լ� ����"))
            {

                myTarget.EventExecute();
            }
        }
    }
#endif
}
