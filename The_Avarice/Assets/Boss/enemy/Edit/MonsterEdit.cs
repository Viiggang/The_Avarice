using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


namespace MonstersEditor
{

    public partial class MonsterEdit : EditorWindow
    {
        MonsterController controller;
        MonsterAniController aniManager;
        MonsterData data;
        List<MonsterAniData> MonsterAniList;
        List<MonsterStates> StatesList;
        List<BaseAniEvent> EventList;

        GameObject cloneRoot;
        GameObject childObj_MonsterHandler;
        GameObject animaction;
        GameObject detectionRange;
        GameObject footCollider;

        private Vector2 leftScroll;
        private Vector2 rightScroll;
        private Vector2 eventScroll;
        private Vector2 aniScroll;
        private Vector2 rScroll;
        private GameObject[] prefabs;
        private GameObject selectedPrefab;
        //bool showStats = false;
        Sprite imagedata;

        private bool statusfold = true;
        private bool Datafold = false;
        [MenuItem("MonsterMaker/Forest/Forest monster Create")]
        public static void OpenWindow()
        {
            var window = GetWindow<MonsterEdit>("MonsterEdit");
            window.position = new Rect(200f, 150f, 1920f, 1080f);
        }

        private void OnEnable()
        {
            // "Assets/ForestMonster/CreateData/Prefab" 폴더의 모든 프리팹 로드
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Boss/enemy/CreateData/Prefab" });
            prefabs = new GameObject[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            }

        }

        private void OnGUI()
        {
            //몬스터 생성
            //////////////////////////////////
            GUILayout.BeginHorizontal();    //                   
            CreateMonster();                //                
            GUILayout.EndHorizontal();      //
            //////////////////////////////////

            // 왼쪽 스크롤 - 프리팹 리스트
            //////////////////////////////////
            GUILayout.BeginHorizontal();    //                      
            MonsterPrefabList();            //                         
            GUILayout.EndScrollView();      //
            //////////////////////////////////
           

            // 오른쪽 스크롤 - 상세 UI
            //////////////////////////////////////////////////////////
            rightScroll = GUILayout.BeginScrollView(rightScroll);   //
            GUILayout.BeginVertical();                              //
                                                                    //
                                                                    //
            if (selectedPrefab != null)                             //
                DrawSelectedPrefabEditor();                         //
                                                                    //
            GUILayout.EndVertical();                                //
            GUILayout.EndScrollView();                              //
            //////////////////////////////////////////////////////////
            GUILayout.EndHorizontal();
        }

       

        void DrawSelectedPrefabEditor()
        {

            GUILayout.Label("선택한 프리팹: " + selectedPrefab.name, EditorStyles.boldLabel);

            var status = selectedPrefab.GetComponentInChildren<MonsterStatus>();
            var state = selectedPrefab.GetComponentInChildren<MonsterController>();
            var objSprite = selectedPrefab.GetComponentInChildren<SpriteRenderer>();
            var animanager = selectedPrefab.GetComponentInChildren<MonsterAniController>();
            var animator = selectedPrefab.GetComponentInChildren<Animator>();
            MonsterAniList = animanager.MonsterAniList;
            var eventData = selectedPrefab.GetComponentInChildren<MonsterAniEvents>();
            StatesList = state.StatesList;
            EventList = eventData.AniEventList;
            if (status == null || status.monsterData == null)
                return;

            statusfold = EditorGUILayout.Foldout(statusfold, "능력치 설정");
            if (statusfold)
            {
                status.monsterData.Monstername = EditorGUILayout.TextField("몬스터 이름", status.monsterData.Monstername);
                status.monsterData.Hp = EditorGUILayout.FloatField("체력", status.monsterData.Hp);
                status.monsterData.Damage = EditorGUILayout.FloatField("공격력", status.monsterData.Damage);
                status.monsterData.MoveSpeed = EditorGUILayout.FloatField("이동속도", status.monsterData.MoveSpeed);
                status.monsterData.PatrolTime = EditorGUILayout.FloatField("순찰시간", status.monsterData.PatrolTime);
                status.monsterData.IdleTime = EditorGUILayout.FloatField("대기시간", status.monsterData.IdleTime);
                status.monsterData.AttackDistance = EditorGUILayout.FloatField("공격거리", status.monsterData.AttackDistance);
                status.monsterData.Defense = EditorGUILayout.IntField("방어력", status.monsterData.Defense);

            }
            animator.runtimeAnimatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("애니메이터 컨트롤러", animator.runtimeAnimatorController, typeof(RuntimeAnimatorController), false);
            // 기존 Sprite를 보여줌
            imagedata = objSprite.sprite;

            // 유저가 선택한 Sprite로 업데이트
            Sprite selectedSprite = (Sprite)EditorGUILayout.ObjectField("몬스터 기본 이미지", imagedata, typeof(Sprite), false);

            // 값이 변경되었다면 반영
            if (selectedSprite != imagedata)
            {
                imagedata = selectedSprite;
                objSprite.sprite = imagedata;  // SpriteRenderer에 반영
            }
            //////////////////////////////////////////////////////////////////////////////////

            Datafold = EditorGUILayout.Foldout(Datafold, "데이터 설정");
            if (Datafold)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                aniScroll = GUILayout.BeginScrollView(aniScroll);
                for (int i = 0; i < MonsterAniList.Count; i++)
                {
                    MonsterAniList[i] = (MonsterAniData)EditorGUILayout.ObjectField($"애니메이션 트리거 리스트:{i + 1}",// 라벨 (string)
                                                                                                   MonsterAniList[i],   // 현재 값 (Object)
                                                                                                   typeof(MonsterAniData),  // 허용 타입 (Type)
                                                                                                   false);
                }
                if (GUILayout.Button("+ Add AniState"))
                {

                    MonsterAniList.Add(new MonsterAniData());
                }
                if (GUILayout.Button("- Remove Last AniState"))
                {
                    if (MonsterAniList.Count == 0) return;

                    MonsterAniList.RemoveAt(MonsterAniList.Count - 1);
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.BeginVertical();

                for (int j = 0; j < StatesList.Count; j++)
                {
                    StatesList[j] = (MonsterStates)EditorGUILayout.ObjectField($"몬스터 상태패턴 리스트:{j + 1}",// 라벨 (string)
                                                                                                   StatesList[j],   // 현재 값 (Object)
                                                                                                   typeof(MonsterStates),  // 허용 타입 (Type)
                                                                                                   false);
                }
                if (GUILayout.Button("+ Add MonsterState"))
                {

                    StatesList.Add(new MonsterStates());
                }
                if (GUILayout.Button("- Remove MonsterState Last Index"))
                {
                    if (StatesList.Count == 0) return;
                    StatesList.RemoveAt(StatesList.Count - 1);
                }
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUILayout.Space(50);
                eventScroll = GUILayout.BeginScrollView(eventScroll, GUILayout.Width(400f));
                for (int j = 0; j < EventList.Count; j++)
                {
                    EventList[j] = (BaseAniEvent)EditorGUILayout.ObjectField($"애니메이션 이벤트 리스트:{j + 1}",// 라벨 (string)
                                                                                                   EventList[j],   // 현재 값 (Object)
                                                                                                   typeof(BaseAniEvent),  // 허용 타입 (Type)
                                                                                                   false);
                }
                if (GUILayout.Button("+ Add"))
                {

                    EventList.Add(new BaseAniEvent());
                }
                if (GUILayout.Button("- Remove"))
                {
                    if (EventList.Count == 0) return;
                    EventList.RemoveAt(EventList.Count - 1);
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }

            //////////////////////////////////////////////////////////////////////////////////

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save Changes"))
            {
                objSprite.sprite = imagedata;

                bool canChangePrefabName = status.monsterData.Monstername.Length != 0;
                if (canChangePrefabName)
                {
                    selectedPrefab.name = status.monsterData.Monstername;
                    status.monsterData.name = status.monsterData.Monstername;
                }
               
                EditorUtility.SetDirty(status.monsterData);
                EditorUtility.SetDirty(animanager);
                EditorUtility.SetDirty(state);
                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("Delete Prefab"))
            {
                string prefabPath = AssetDatabase.GetAssetPath(selectedPrefab);
                string dataPath = "Assets/enemy/Forest/commonscript/CreateData/data/" + selectedPrefab.name + ".asset";

                AssetDatabase.DeleteAsset(prefabPath);
                AssetDatabase.DeleteAsset(dataPath);

                AssetDatabase.Refresh();

                selectedPrefab = null;
                OnEnable();
            }
        }



      

     
    }

}
