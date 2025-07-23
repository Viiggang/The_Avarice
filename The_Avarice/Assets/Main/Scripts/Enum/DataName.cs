

using System.Runtime.Serialization;


[System.Serializable]
public enum DataName
{
    [EnumDisplayName("첫 번째 씬 데이터")]
    SceneOneData,

}
/*
 스크립트 오브젝트로 만든 데이터를 가져올때 데이터 이름을  enum으로 만든 후 
tostring()으로 반환
 */