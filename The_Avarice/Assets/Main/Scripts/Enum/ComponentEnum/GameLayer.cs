public enum GameLayer
{
    //게임 처음 씬
    [EnumDisplayName("배경")]
    BackGround = 0,
    [EnumDisplayName("타이틀")]
    Title,
    [EnumDisplayName("게임시작")]
    GameStart,
    [EnumDisplayName("게임옵션")]
    GameOption,
    [EnumDisplayName("게임 종료")]
    Exit,
    
    //캐릭터 선택 씬
    SelectOnetCharator,
    SelectTwoCharator,
    SelectThreeCharator,
    SelectFourCharator,

}

