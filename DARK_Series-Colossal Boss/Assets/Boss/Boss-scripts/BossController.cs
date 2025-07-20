using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private IBossState currentState;

    public Transform target;
    public BoxCollider2D Collider;
    public Animator animator;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    void Start()
    {
        ChangeState(new SleepState());
    }

    void Update()
    {
        currentState?.Execute(this);
    }

    public void ChangeState(IBossState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    // 예: 체력 감소 등에서 상태 전환 가능
    public void Die()
    {
        ChangeState(new DeadState());
    }
}
