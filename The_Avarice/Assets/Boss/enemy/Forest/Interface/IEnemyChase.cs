using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyChase
{
    public void Chase(Transform self, Transform target, float speed);

}
 