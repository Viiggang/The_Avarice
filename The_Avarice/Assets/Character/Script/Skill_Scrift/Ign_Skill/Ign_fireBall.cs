using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[DisallowMultipleComponent]
public class Ign_fireBall : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    [SerializeField] 
    private float lifeTime = 3f;
    [SerializeField] 
    private Animator animator;
    [SerializeField] 
    private LayerMask hitMask;

    private Vector2 direction;
    private bool isFlying;
    private float lifeTimer;
    private float AtkDamage = 1f;
    private Vector3 initialPosition;

    public bool IsActive => gameObject.activeSelf;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        initialPosition = transform.position;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        lifeTimer = 0f;
    }

    private void Update()
    {
        if (!isFlying) return;

        // 이동
        transform.Translate(direction * speed * Time.deltaTime);

        // 라이프타임 체크
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            StopAndDespawn();
        }
    }

    public void Launch(Vector2 dir, Vector3 startPos)
    {
        transform.position = startPos;
        direction = dir.normalized;
        lifeTimer = 0f;
        isFlying = false;

        gameObject.SetActive(true);
        animator.SetTrigger("Spawn");
    }

    public void OnSpawnEnd()
    {
        isFlying = true;
        animator.SetTrigger("Fly");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFlying) return;

        if (((1 << other.gameObject.layer) & hitMask) != 0)
        {
            IDamage damage = other.GetComponent<IDamage>(); //충돌한 오브젝트에서 IDamage 인터페이스를 가져옮
            if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 충돌한 오브젝트가 IDamage인터페이스를 가지고있고 레이어가 enemy이라면
            {
                damage.OnHitDamage(AtkDamage); //인터페이스에 선언된 OnHitDamage()메소드를 호출 = 피격처리
            }

                StopAndDespawn();
        }
    }

    private void StopAndDespawn()
    {
        if (!gameObject.activeSelf) return;

        isFlying = false;
        animator.SetTrigger("Despawn");
    }

    public void OnDespawnEnd()
    {
        gameObject.SetActive(false);
        transform.position = initialPosition;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        if (dir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x > 0) transform.localScale = new Vector3(1, 1, 1);
    }
}
