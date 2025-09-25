using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

namespace MonstersEditor
{
    public partial class MonsterEdit
    {
        public void MonsterPrefabList()
        {
            // 왼쪽 스크롤 - 프리팹 리스트
            leftScroll = GUILayout.BeginScrollView(leftScroll, GUILayout.Width(200));
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("몬스터 목록", EditorStyles.boldLabel);
            foreach (var prefab in prefabs)
            {
                if (prefab == null) continue;
                if (GUILayout.Button(prefab.name))
                {
                    selectedPrefab = prefab;
                    
                }
            }
        }
    }
}

