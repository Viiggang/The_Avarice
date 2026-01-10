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
        //보스 현재 체력 및 풀피 체력
        public BossStatus BossHp; //<-- 나중에 보스 체력 status로 변경
        public float MaxHp;

        //HpLine  최대 길이
        public float maxwidth;

        //HpUI
        public RectTransform HpLine;

        //페이즈 2 돌입시 작동 시킬 함수들
        public event Action OnPhase2Start;

        //Hp 0이되면 캔버스 삭제용 변수
        public GameObject DestroyCanvas;

        //활성화 시킬 오브젝트
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
            //페이지 2,체력 절반이 되면 UI 켜지는데 이때 한번 실행
            UpdateUI();
        }

        public void EventExecute()//등록한 이벤트 처리
        {
            OnPhase2Start?.Invoke();
        }

        public void ShowBossHealthUIIfHalf()//UI를 보여줄지 판단.
        {
          
            bool Onable = BossHp.hp <= (MaxHp / 2);
            if (Onable && !hpUIShown)
            {
                hpbar.SetActive(true);
                hpUIShown = true;
                OnPhase2Start -= ShowBossHealthUIIfHalf;
            }
        }
        public void UpdateUI()//몬스터 피격당할 때마다 UI업데이터 해줌
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
            DrawDefaultInspector();  // 기본 인스펙터 그리기

            var myTarget = (UIManager)target;

            if (GUILayout.Button("UI  업데이트"))
            {
                myTarget.UpdateUI();
            }
            if (GUILayout.Button("오브젝트 페이즈 2 돌입"))
            {
                myTarget.BossHp.hp = (myTarget.BossHp.MaxHp / 2);
                myTarget.EventExecute();
            }
            if (GUILayout.Button("함수 실행"))
            {

                myTarget.EventExecute();
            }
        }
    }
#endif
}
