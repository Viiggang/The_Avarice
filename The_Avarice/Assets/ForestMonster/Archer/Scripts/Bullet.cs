using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // ÃÑ¾Ë ¼Óµµ
    public Transform target;
    public Vector3 dir;
    float angle;
    void Start()
    {
        dir = (target.position - transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}
