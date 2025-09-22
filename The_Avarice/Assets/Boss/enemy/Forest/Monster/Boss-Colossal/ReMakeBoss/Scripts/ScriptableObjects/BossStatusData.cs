using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStatusData", menuName = "BossStatusData")]
public class BossStatusData : ScriptableObject
{
    public string bossName;//���� �̸� 
    public float hp;//ü��
    public float speed;//�̵��ӵ�
    public int defense;//����
    [TextArea]public string descripcion;
}
