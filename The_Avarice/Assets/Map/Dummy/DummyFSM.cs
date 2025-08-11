using UnityEngine;

public class DummyFSM : MonoBehaviour
{
    private State currentState;

    public Animator animator;
    public int hp = 100;

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        if (hp <= 0)
        {
            ChangeState(new DeadState(this));
        }
        else
        {
            ChangeState(new HitState(this));
        }
    }
}

public abstract class State
{
    protected DummyFSM fsm;

    protected State(DummyFSM fsm)
    {
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class IdleState : State
{
    public IdleState(DummyFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        fsm.animator.Play("Idle");
    }
}

public class HitState : State
{
    private float hitDuration = 0.5f;
    private float timer;

    public HitState(DummyFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        fsm.animator.Play("Hit");
        timer = hitDuration;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            fsm.ChangeState(new IdleState(fsm));
        }
    }
}

public class DeadState : State
{
    public DeadState(DummyFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        fsm.animator.Play("Dead");
        // 죽으면 아무 동작 안 함
    }
}
