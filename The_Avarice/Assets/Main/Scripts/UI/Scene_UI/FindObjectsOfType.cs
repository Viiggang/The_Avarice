 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static Unity.VisualScripting.Metadata;
//using testddd;
namespace Leein
{
    public class FindObjectsOfType : MonoBehaviour// 
    {

       
        [SerializeField]
        SceneData LoadData;
        [SerializeField]
        Font Font;
        [SerializeField]
        Sprite Sprite;

        [Leein.InspectorName("씬 데이터")]
        public DataName LoadDataName;

        UIScreenBuilder Builder;
        private void Awake()
        {
            Builder = new UIScreenBuilder();
        }
        void Start()
        {
            ResourceLoad.LoadSceneResources(ref LoadData, ref Font, ref Sprite, LoadDataName);
            Builder.SetUp(LoadData, Font, Sprite);
            Builder.CreateLayerDictionary();
            ApplyLayerDictionary();

        }

        void ApplyLayerDictionary()
        {
            var allTransforms = CustomComponent.SafeFindObjectsOfType<GameLayerMarker>();

            foreach (var obj in allTransforms)
            {
                if (Builder.LayerActions.TryGetValue(obj.gameLayer, out var action))
                {
                    action(obj.gameObject); // 해당 GameObject를 기반으로 처리
                }

            }
        }



    }

}
