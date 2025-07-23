
 
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace Leein
{

    public class UIScreenBuilder
    {
        int SIZE = 50;
        SceneData m_ResourceName;
        Font m_loadedTextFont;
        Sprite m_loadedImage;
        public Dictionary<GameLayer, Action<GameObject>> LayerActions;
        public void SetUp(SceneData ResourceName, Font loadedTextFont, Sprite loadedImage)
        {
            m_ResourceName = ResourceName;
            m_loadedTextFont = loadedTextFont;
            m_loadedImage = loadedImage;
        }
        public void SetBackGround(GameObject ChildObject)
        {
     
            var Image = ChildObject.SafeGetComponentInChildren<Image>("image");
            var BackGroundImage = CustomResourceLoad.LoadSprite(m_ResourceName.BackGroundImageItem.ToString());


            if (BackGroundImage != null)
            {
                Image.sprite = BackGroundImage;//배경이미지 설정
                Image.SetNativeSize();
            }
            else
            {
                Debug.LogError($"에러 오브젝트 이름:{ChildObject.name}");
            }

        }

        public void SetTitle(GameObject ChildObject)
        {

             
            var Text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            rectTransform.anchoredPosition = new Vector2(-536, 354);
            Text.text = "The Avarice";//메인 타이틀 제목 설정
            Text.fontSize = 250;
            Text.font = m_loadedTextFont;
        }

        public void SetGameStart(GameObject ChildObject)
        {
            
            var Image = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button = ChildObject.SafeGetComponent<Button>("Button");

            text.text = "게임시작";
            text.fontSize = SIZE;
            text.font = m_loadedTextFont;
            Image.sprite = m_loadedImage;
            button.onClick.AddListener(ButoonFun.Gamestart);
            rectTransform.anchoredPosition = new Vector2(-27, -286);
        }


        public void SetGameOption(GameObject ChildObject)
        {
             
            var Image1 = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform1 = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text1 = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button1 = ChildObject.SafeGetComponent<Button>("Button");

            text1.text = "옵션";
            text1.fontSize = SIZE;
            text1.font = m_loadedTextFont;
            Image1.sprite = m_loadedImage;
            button1.onClick.AddListener(ButoonFun.ShowGameoption);
            rectTransform1.anchoredPosition = new Vector2(-27, -386);
        }


        public void SetExit(GameObject ChildObject)
        {
            
            var Image = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button = ChildObject.SafeGetComponent<Button>("Button");

            text.text = "종료";
            text.fontSize = SIZE;
            text.font = m_loadedTextFont;
            Image.sprite = m_loadedImage;
            button.onClick.AddListener(ButoonFun.GameExit);
            rectTransform.anchoredPosition = new Vector2(-27, -486);
        }
        public void CreateLayerDictionary()
        {
            LayerActions = new Dictionary<GameLayer, Action<GameObject>>
            {
                { GameLayer.BackGround,  SetBackGround },
                { GameLayer.Title,      SetTitle },
                { GameLayer.GameStart,  SetGameStart },
                { GameLayer.GameOption, SetGameOption },
                { GameLayer.Exit,       SetExit }
             };
        }
    }
}
