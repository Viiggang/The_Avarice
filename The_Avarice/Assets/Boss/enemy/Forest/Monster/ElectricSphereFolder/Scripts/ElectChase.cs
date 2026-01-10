using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectChase : IEnemyChase
{

    public void Chase(Transform self, Transform target, float speed)
    {
        float dir = Mathf.Sign(target.position.x - self.position.x);
        if (Mathf.Abs(self.position.x - target.position.x) > 0.3)
        {
            Vector3 newPos = self.position + new Vector3(dir * speed * Time.deltaTime, 0f, 0f);
            self.position = newPos;

        }
    }
}