using UnityEngine;

public class DummyFSM : MonoBehaviour, IDamage
{
    private D_State currentState;

    public Animator animator;

    [Header("VFX Settings")]
    public GameObject hitVFXPrefab;
    public GameObject stunVFXPrefab;
    public Transform vfxSpawnPoint;


    private void Start()
    {
        ChangeState(new D_IdleState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(D_State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void OnHitDamage(float damage)
    {
        ChangeState(new D_HitState(this, hitVFXPrefab, vfxSpawnPoint));
    }

    public void TakeStun(float duration)
    {
        if (stunVFXPrefab != null && vfxSpawnPoint != null)
        {
            GameObject vfxInstance = GameObject.Instantiate(stunVFXPrefab, vfxSpawnPoint.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
            GameObject.Destroy(vfxInstance, duration);
        }
    }
}

public abstract class D_State
{
    protected DummyFSM fsm;

    protected D_State(DummyFSM fsm)
    {
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class D_IdleState : D_State
{
    public D_IdleState(DummyFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        fsm.animator.Play("Idle");
    }
}

public class D_HitState : D_State
{
    private float hitDuration = 0.5f;
    private float timer;

    // VFX Prefab
    private GameObject hitVFX;
    private Transform vfxSpawnPoint;
    public D_HitState(DummyFSM fsm) : base(fsm) { }

    public D_HitState(DummyFSM fsm, GameObject hitVFXPrefab, Transform spawnPoint) : base(fsm)
    {
        hitVFX = hitVFXPrefab;
        vfxSpawnPoint = spawnPoint;
    }

    public override void Enter()
    {
        fsm.animator.Play("Hit");

        if (hitVFX != null && vfxSpawnPoint != null)
        {
            GameObject vfxInstance = GameObject.Instantiate(hitVFX, vfxSpawnPoint.position, Quaternion.identity);
            GameObject.Destroy(vfxInstance, 0.5f);
        }

        timer = hitDuration;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            fsm.ChangeState(new D_IdleState(fsm));
        }
    }
}
