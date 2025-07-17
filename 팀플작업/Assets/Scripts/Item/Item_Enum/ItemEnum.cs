

//아이템 타입
public enum ItemEnum 
{
   NULL =0,
    equipment, //장비
    Consumption, //소비
}

//장비 타입 (무기 모자 상의 하의 신발 장갑 기타 등등)
//장비-무기  장비-모자 이런식
public enum equipmentEnum
{
    NULL =0,
    equipment_Weapon,// 무기
    equipment_hat,//모자
}

//소비 타입 (회복 버프 등등 )
public enum ConsumptionEnum
{
    NULL =0,
    Consumption_Heal, //회복

}
