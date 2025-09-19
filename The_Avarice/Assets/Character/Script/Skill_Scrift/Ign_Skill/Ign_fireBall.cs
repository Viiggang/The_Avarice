using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
[DisallowMultipleComponent]
public class ProjectileKinematic : MonoBehaviour, IPoolable
{
    [Header("Motion")]
    public float defaultSpeed = 25f;
    public float defaultLifetime = 3f;
    [Tooltip("�� ������Ʈ�� �ٸ� ������Ʈ�� �浹 �˻��� ���̾�")]
    public LayerMask hitLayers = ~0;

    [Header("Collision / Physics")]
    [Tooltip("���ǵ尡 ���� �� ����(�ͳθ�) ������ ������")]
    public float skinWidth = 0.05f;
    [Tooltip("Rigidbody�� �浹 ���� ���(����: ContinuousSpeculative)")]
    public CollisionDetectionMode preferredCollisionDetection = CollisionDetectionMode.ContinuousSpeculative;

    [Header("Hit / Animation")]
    public Animator animator;                    
    public string hitTriggerName = "Hit";
    public float hitAnimationFallback = 0.4f;    
    public string hitClipHint = "Hit";
    public float AtkDamage = 5f;

    Rigidbody rb;
    Collider[] collidersCache;
    Vector3 moveDirection = Vector3.forward;
    float moveSpeed;
    float lifeTimer;
    float lifeTime;
    bool isMoving;
    bool isHit;
    Coroutine hitCoroutine;

    #region IPoolable
    public void OnSpawn()
    {
        lifeTimer = 0f;
        isMoving = false;
        isHit = false;

        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = preferredCollisionDetection;

        EnsureColliders(true);

        if (animator != null) animator.ResetTrigger(hitTriggerName);

        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }
    }

    public void OnDespawn()
    {
        isMoving = false;
        isHit = false;

        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }

        if (animator != null) animator.ResetTrigger(hitTriggerName);

        EnsureColliders(true);
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collidersCache = GetComponentsInChildren<Collider>(true);

        if (rb == null)
            Debug.LogError("[ProjectileKinematic] Rigidbody�� �ʿ��մϴ� (RequireComponent�� ����Ǿ�� ��).");
    }
    public void Launch(Vector3 direction, float speed = -1f, float lifetime = -1f)
    {
        if (direction.sqrMagnitude <= 0f) direction = transform.forward;
        moveDirection = direction.normalized;
        moveSpeed = (speed > 0f) ? speed : defaultSpeed;
        lifeTime = (lifetime > 0f) ? lifetime : defaultLifetime;

        lifeTimer = 0f;
        isMoving = true;
        isHit = false;

        EnsureColliders(true);

        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = preferredCollisionDetection;
    }

    private void FixedUpdate()
    {
        if (!isMoving || isHit) return;

        float dt = Time.fixedDeltaTime;
        var step = moveSpeed * dt;
        if (step <= 0f) return;


        Vector3 current = rb.position;
        Vector3 next = current + moveDirection * step;

        // �̸� Raycast�� �ͳθ� ����(���� ������Ʈ�� ��� �߰� ����)
        RaycastHit hit;
        if (Physics.Raycast(current, moveDirection, out hit, step + skinWidth, hitLayers, QueryTriggerInteraction.Collide))
        {

            rb.MovePosition(hit.point);
            HandleHitInternal(hit.collider, hit.point, hit.normal);
        }
        else
        {
            // ���� �̵�
            rb.MovePosition(next);

            // ���� üũ (FixedUpdate���� ����)
            lifeTimer += dt;
            if (lifeTimer >= lifeTime)
            {
                HandleTimeoutInternal();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;
        HandleHitInternal(other, transform.position, -transform.forward);
        IDamage damage = other.GetComponent<IDamage>(); //�浹�� ������Ʈ���� IDamage �������̽��� ������
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // �浹�� ������Ʈ�� IDamage�������̽��� �������ְ� ���̾ enemy�̶��
        {
            damage.OnHitDamage(AtkDamage); //�������̽��� ����� OnHitDamage()�޼ҵ带 ȣ�� = �ǰ�ó��
            if (PlayerMgr.instance.getPlayerType() == Player_Type.Paladin && !PlayerMgr.instance.getonPassive())
            {
                PlayerMgr.instance.sumPassiveStack(1);
            }
        }
    }
    #region Hit / Timeout handling

    private void HandleHitInternal(Collider other, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isHit) return;
        isHit = true;
        isMoving = false;


        EnsureColliders(false);

        
        // �ִϸ����� ó��
        if (animator != null)
        {
            animator.ResetTrigger(hitTriggerName);
            animator.SetTrigger(hitTriggerName);

            float wait = FindLikelyHitClipLength();
            if (wait <= 0f) wait = hitAnimationFallback;

            if (hitCoroutine != null) StopCoroutine(hitCoroutine);
            hitCoroutine = StartCoroutine(WaitAndReturnAfterSeconds(wait));
        }
        else
        {
            PoolMgr.Instance.Release(gameObject);
        }
    }

    private void HandleTimeoutInternal()
    {
        if (isHit) return;
        isHit = true;
        isMoving = false;

        EnsureColliders(false);

        if (animator != null)
        {
            animator.ResetTrigger(hitTriggerName);
            animator.SetTrigger(hitTriggerName);

            float wait = FindLikelyHitClipLength();
            if (wait <= 0f) wait = hitAnimationFallback;

            if (hitCoroutine != null) StopCoroutine(hitCoroutine);
            hitCoroutine = StartCoroutine(WaitAndReturnAfterSeconds(wait));
        }
        else
        {
            PoolMgr.Instance.Release(gameObject);
        }
    }

    private IEnumerator WaitAndReturnAfterSeconds(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        hitCoroutine = null;
        PoolMgr.Instance.Release(gameObject);
    }

    public void OnHitAnimationComplete()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }
        PoolMgr.Instance.Release(gameObject);
    }

    private float FindLikelyHitClipLength()
    {
        if (animator == null) return -1f;
        var rc = animator.runtimeAnimatorController;
        if (rc == null) return -1f;
        var clips = rc.animationClips;
        if (clips == null || clips.Length == 0) return -1f;

        for (int i = 0; i < clips.Length; i++)
            if (string.Equals(clips[i].name, hitClipHint, System.StringComparison.OrdinalIgnoreCase))
                return clips[i].length;
        for (int i = 0; i < clips.Length; i++)
            if (clips[i].name.IndexOf(hitClipHint, System.StringComparison.OrdinalIgnoreCase) >= 0)
                return clips[i].length;
        return -1f;
    }

    #endregion

    private void EnsureColliders(bool enabled)
    {
        if (collidersCache == null) collidersCache = GetComponentsInChildren<Collider>(true);
        for (int i = 0; i < collidersCache.Length; i++)
        {
            var c = collidersCache[i];
            if (c != null) c.enabled = enabled;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.05f);
        if (isMoving)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, moveDirection.normalized * 0.5f);
        }
    }
#endif
}
