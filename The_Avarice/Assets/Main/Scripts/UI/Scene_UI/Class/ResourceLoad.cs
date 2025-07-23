
using UnityEngine;

namespace Leein
{
    public static class ResourceLoad
    {
        public static void LoadSceneResources(ref SceneData SceneDataResource, ref Font loadedTextFont, ref Sprite loadedImage, DataName LoadData)
        {
            //  ���� ��ũ��Ʈ������Ʈ�� ���� �ҷ�����
            DataName LoadDataName = LoadData;
            SceneDataResource = CustomResourceLoad.LoadScriptObject<SceneData>(LoadDataName.ToString());
            if (SceneDataResource == null)
            {
                Debug.Log($"���ҽ� ResourceName");
                return;
            }

            //����� �����ͷ� �̹��� ����
            loadedImage = CustomResourceLoad.LoadSprite(SceneDataResource.FontItem.ToString());
            if (loadedImage == null)
            {
                Debug.Log($"���ҽ� loadedImage");
                return;
            }

            //��Ʈ ����
            loadedTextFont = CustomResourceLoad.LoadFont(SceneDataResource.buttonItem.ToString());
            if (loadedTextFont == null)
            {
                Debug.Log($"���ҽ� loadedTextFont");
                return;
            }
        }

    }
}

