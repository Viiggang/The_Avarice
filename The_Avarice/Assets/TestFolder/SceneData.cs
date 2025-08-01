using UnityEngine;

[CreateAssetMenu(menuName = "Data/SceneData")]
public class SceneData : ScriptableObject
{ 
    [Header("버튼이미지")]public buttonImagePath buttonItem;
    [Header("배경이미지")]public BackGroundImagePath BackGroundImageItem;
    [Header("폰트이미지")]public FontPath FontItem;
}
