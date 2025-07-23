
using UnityEngine;

namespace Leein
{
    public static class ResourceLoad
    {
        public static void LoadSceneResources(ref SceneData SceneDataResource, ref Font loadedTextFont, ref Sprite loadedImage, DataName LoadData)
        {
            //  먼저 스크립트오브젝트로 값을 불러오고
            DataName LoadDataName = LoadData;
            SceneDataResource = CustomResourceLoad.LoadScriptObject<SceneData>(LoadDataName.ToString());
            if (SceneDataResource == null)
            {
                Debug.Log($"리소스 ResourceName");
                return;
            }

            //저장된 데이터로 이미지 셋팅
            loadedImage = CustomResourceLoad.LoadSprite(SceneDataResource.FontItem.ToString());
            if (loadedImage == null)
            {
                Debug.Log($"리소스 loadedImage");
                return;
            }

            //폰트 셋팅
            loadedTextFont = CustomResourceLoad.LoadFont(SceneDataResource.buttonItem.ToString());
            if (loadedTextFont == null)
            {
                Debug.Log($"리소스 loadedTextFont");
                return;
            }
        }

    }
}

