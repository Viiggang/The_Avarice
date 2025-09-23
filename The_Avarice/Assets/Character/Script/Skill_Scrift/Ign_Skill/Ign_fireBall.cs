using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[DisallowMultipleComponent]
public class ProjectileKinematic2D : MonoBehaviour, IPoolable
{
    [Header("Motion")]
    public float defaultSpeed = 25f;
    public float defaultLifetime = 3f;
    [Tooltip("충돌할 레이어")]
    public LayerMask hitLayers = ~0;

    [Header("Collision / Physics")]
    [Tooltip("터널링 방지 여유값")]
    public float skinWidth = 0.05f;
    [Tooltip("Rigidbody2D 충돌 감지 모드")]
    public CollisionDetectionMode2D preferredCollisionDetection = CollisionDetectionMode2D.Continuous;

    [Header("Hit / Animation")]
    public Animator animator;
    public string hitTriggerName = "Hit";
    public float hitAnimationFallback = 0.4f;
    public string hitClipHint = "Hit";
    public float AtkDamage = 5f;

    Rigidbody2D rb;
    Collider2D[] collidersCache;
    Vector2 moveDirection = Vector2.right;
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

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
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
        rb = GetComponent<Rigidbody2D>();
        collidersCache = GetComponentsInChildren<Collider2D>(true);

        if (rb == null)
            Debug.LogError("[ProjectileKinematic2D] Rigidbody2D가 필요합니다.");
    }

    /// <summary>
    /// 발사 함수 (발사 순간에만 방향을 바라봄)
    /// </summary>
    public void Launch(Vector2 direction, float speed = -1f, float lifetime = -1f)
    {
        if (direction.sqrMagnitude <= 0f) direction = Vector2.right;

        moveDirection = direction.normalized;
        moveSpeed = (speed > 0f) ? speed : defaultSpeed;
        lifeTime = (lifetime > 0f) ? lifetime : defaultLifetime;

        lifeTimer = 0f;
        isMoving = true;
        isHit = false;

        EnsureColliders(true);

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = preferredCollisionDetection;

        // 발사 방향을 바라보도록 회전
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        if (!isMoving || isHit) return;

        float dt = Time.fixedDeltaTime;
        float step = moveSpeed * dt;
        if (step <= 0f) return;

        Vector2 current = rb.position;
        Vector2 next = current + moveDirection * step;

        RaycastHit2D hit = Physics2D.Raycast(current, moveDirection, step + skinWidth, hitLayers);
        if (hit.collider != null)
        {
            rb.MovePosition(hit.point);
            HandleHitInternal(hit.collider, hit.point, hit.normal);
        }
        else
        {
            rb.MovePosition(next);

            lifeTimer += dt;
            if (lifeTimer >= lifeTime)
            {
                HandleTimeoutInternal();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHit) return;

        HandleHitInternal(other, transform.position, -transform.right);

        IDamage damage = other.GetComponent<IDamage>();
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            damage.OnHitDamage(AtkDamage);
            if (PlayerMgr.instance.playerType == Player_Type.Paladin && !PlayerMgr.instance.OnPassive)
            {
                PlayerMgr.instance.sumPassiveStack(1);
            }
        }
    }

    #region Hit / Timeout handling
    private void HandleHitInternal(Collider2D other, Vector2 hitPoint, Vector2 hitNormal)
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
            PoolMgr.instance.Release(gameObject);
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
            PoolMgr.instance.Release(gameObject);
        }
    }

    private IEnumerator WaitAndReturnAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        hitCoroutine = null;
        PoolMgr.instance.Release(gameObject);
    }

    public void OnHitAnimationComplete()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }
        PoolMgr.instance.Release(gameObject);
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
        if (collidersCache == null) collidersCache = GetComponentsInChildren<Collider2D>(true);
        foreach (var c in collidersCache)
        {
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
