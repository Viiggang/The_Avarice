//using ElectricSphere;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEditor.PlayerSettings;
//using static UnityEngine.GraphicsBuffer;

//public class AniEvent : MonoBehaviour
//{
//    private bool flag;

//    public Vector3 size;
//    public Vector3 pos;
//    public LayerMask player;
//    public void OnDrawGizmos()
//    {
//        pos= this.transform.position;
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireCube(pos,size);
//    }
//    public void Rechase()
//    {

//        SphereHandler.Instance.EmTransform.chase = true;
//    }
//    public void Attack1()
//    {
//        size = new Vector3(-0.61f, 2.13f,0f);
//        var hit= Physics2D.OverlapBox(pos, size,0f, player);
//        if (hit == null) return;

//        var data = hit.gameObject.GetComponent<IDamage>();
//        if (data == null) return;

//        data.OnHitDamage(1f);

//    }
//    public void Attack2()
//    {
//        size = new Vector3(-0.61f, 2.13f, 0f);
//        var hit = Physics2D.OverlapBox(pos, size, 0f, player);
//        if (hit == null) return;

//        var data = hit.gameObject.GetComponent<IDamage>();
//        if (data == null) return;

//        data.OnHitDamage(1f);
//    }
  
//}
