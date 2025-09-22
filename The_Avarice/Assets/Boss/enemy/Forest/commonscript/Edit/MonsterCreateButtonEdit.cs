using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

namespace MonstersEditor
{
    public partial class MonsterEdit
    {
        public void CreateMonster()
        {
            if (GUILayout.Button("���� �����ϱ�"))
            {
                selectedPrefab = null;
                imagedata = null;
                data = ScriptableObject.CreateInstance<MonsterData>();
                
                CreateClone();
                DrawNewMonsterEditor();
            }
        }
        private void CreateClone()
        {
            cloneRoot = new GameObject("Monster");
            cloneRoot.AddComponent<Rigidbody2D>();

            childObj_MonsterHandler = new GameObject("MonsterHandler");
            childObj_MonsterHandler.transform.SetParent(cloneRoot.transform, false);
            childObj_MonsterHandler.layer = 6;

            var managerComp = childObj_MonsterHandler.AddComponent<MonsterController>();
            var aniManagerComp = childObj_MonsterHandler.AddComponent<MonsterAniController>();
            var statusComp = childObj_MonsterHandler.AddComponent<MonsterStatus>();
            statusComp.AniManager = aniManagerComp;
            statusComp.monsterData = data;
            var handlerCollider = childObj_MonsterHandler.AddComponent<BoxCollider2D>();
            handlerCollider.isTrigger = true;

            animaction = new GameObject("Animaction");
            animaction.transform.SetParent(childObj_MonsterHandler.transform, false);
            animaction.AddComponent<MonsterAniEvents>();
            var animator = animaction.AddComponent<Animator>();
            var spriteRenderer = animaction.AddComponent<SpriteRenderer>();


            detectionRange = new GameObject("DetectionRange");
            detectionRange.transform.SetParent(childObj_MonsterHandler.transform, false);
            var detectComp = detectionRange.AddComponent<MsDetectionRange>();
            detectComp.MonsterTrans = cloneRoot.transform;
            detectComp.renderer = spriteRenderer;
            detectComp.Collider = handlerCollider;

            footCollider = new GameObject("FootCollider");
            footCollider.transform.SetParent(childObj_MonsterHandler.transform, false);
            footCollider.AddComponent<BoxCollider2D>();

            statusComp.BoxCollider2D = handlerCollider;
            statusComp.spriteRenderer = spriteRenderer;
            aniManagerComp.animator = animator;

            managerComp.aniManager = aniManagerComp;
            managerComp.statusManager = statusComp;
            managerComp.Detection = detectComp;
            managerComp.MonsterTrans = cloneRoot.transform;

            controller = managerComp;
            aniManager = aniManagerComp;
        }
        void DrawNewMonsterEditor()
        {
            GeneratePrefab();
            OnEnable();
           
        }
        private void GeneratePrefab()
        {
            if (cloneRoot == null || data == null)
            {
                Debug.LogError("������ ���� ����: cloneRoot �Ǵ� data�� null�Դϴ�.");
                return;
            }

            cloneRoot.name = "CreateMonster";

            // ���� ������ ����
            string assetFolder = "Assets/enemy/Forest/commonscript/CreateData/Data/";
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{assetFolder}{cloneRoot.name}.asset");
            AssetDatabase.CreateAsset(data, assetPath);
            AssetDatabase.SaveAssets();

            // ������ ���� ��� ���� �� �ߺ� ���� ó��
            string prefabFolder = "Assets/enemy/Forest/commonscript/CreateData/Prefab/";
            string prefabPath = AssetDatabase.GenerateUniqueAssetPath($"{prefabFolder}{cloneRoot.name}.prefab");
            PrefabUtility.SaveAsPrefabAsset(cloneRoot, prefabPath);



            // ����
            DestroyImmediate(cloneRoot);
            controller = null;
            aniManager = null;
            data = null;
            childObj_MonsterHandler = null;
            animaction = null;
            detectionRange = null;
            footCollider = null;
            imagedata = null;
        }
    }
}

