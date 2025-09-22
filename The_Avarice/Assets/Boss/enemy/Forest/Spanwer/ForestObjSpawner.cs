using ElectricSphere;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class ForestObjSpawner : MonoBehaviour
{
    public GameObject[] OBJ;
    //0 은 보스
    //1 은 전기 구체
    //2~ 몬스터 

    public Transform PlayerPos;

    public Vector2 Pos;
    public Vector2 Size;

    public LayerMask player;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Pos, Size);
        var hit = Physics2D.OverlapBox(Pos, Size, 0f, player);
        PlayerPos = hit.GetComponent<Transform>();
    }
    private void OnEnable()
    {
        var hit = Physics2D.OverlapBox(Pos, Size, 0f, player);
        PlayerPos = hit.GetComponent<Transform>();
        Spawner();
    }
    void Start()
    {
       
    }
    public void Spawner()
    {
        BossStage getstage=new();
        foreach (var obj in OBJ)
        {
            var ObjData = Instantiate(obj);
            var stagedata = ObjData.GetComponentInChildren<BossStage>();
            if(stagedata !=null)
            {
                getstage=stagedata;
            }
           var Elect= ObjData.GetComponentInChildren<EnemyElectric>();
            if(Elect !=null)
            {
                Elect.bosspage = getstage;
            }


            var Data= ObjData.GetComponentInChildren<ITarget>();
            if (Data != null)
                Data.target = PlayerPos;
            else
                continue;
        }

    }
  



}
