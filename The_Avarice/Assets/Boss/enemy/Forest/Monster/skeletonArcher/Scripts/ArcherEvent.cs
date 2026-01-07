using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[CreateAssetMenu (fileName = "ArcherAttack", menuName = "Monster/AnimationEvents/ArcherAttack")]
public class ArcherAttack :BaseAniEvent
{
    public override void Execute(List<GameObject> bulletList, BulletPos bulletPos)
    {
       
        
 
        foreach (GameObject bullet in bulletList)
        {
            if (bullet == null) continue;

            bool activeSelf= bullet.activeSelf;
            if (!activeSelf)
            {
               // bullet.transform.position = pos.transform.position;
                bullet.transform.rotation = Quaternion.identity;
                bulletPos.SetBulletPos();
                bullet.transform.position= bulletPos.transform.position;
                bullet.gameObject.SetActive(true);
                return;
            }
        }
    }
}

