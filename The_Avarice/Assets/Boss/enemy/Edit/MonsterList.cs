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
            // ���� ��ũ�� - ������ ����Ʈ
            leftScroll = GUILayout.BeginScrollView(leftScroll, GUILayout.Width(200));
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("���� ���", EditorStyles.boldLabel);
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

