using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_LightCut : MonoBehaviour
{
    [Header("Hitbox Points")]
    public Transform startPoint;   // 애니메이션에서 시작 위치
    public Transform endPoint;     // 애니메이션에서 끝 위치

    [Header("Hitbox Settings")]
    public float thickness = 0.2f; // 판정 두께
    public LayerMask targetLayer;  // 타격 대상 레이어
    public float activeTime = 0.05f; // 판정 유지 시간 (초)

    public Animator animator;
    public string attackStateName = "Pal_LightCut"; // 공격 애니메이션 상태명

    public float minLength = 0.1f;  // 최소 충돌 길이
    public float maxLength = 2f;    // 최대 충돌 길이

    float AtkDamage = 5f;

    private bool isActive = false;
    private float timer = 0f;
    private float currentLength = 0f;
    public float speedMultiplier = 2f;



    void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isActive = false;
            this.gameObject.SetActive(false);
            return;
        }

        UpdateLengthByAnimation();
        CheckHit();
    }

    public void ActivateHitbox()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            activeTime = clipInfo[0].clip.length;  // 애니메이션 전체 길이로 세팅
        }
        isActive = true;
        timer = activeTime;
        this.gameObject.SetActive(true);
    }

    private void UpdateLengthByAnimation()
    {
        if (animator == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(attackStateName)) return;
        float progress = Mathf.Clamp01((stateInfo.normalizedTime % 1) * speedMultiplier);

        currentLength = Mathf.Lerp(minLength, maxLength, progress);
    }

    private void CheckHit()
    {
        if (startPoint == null || endPoint == null) return;

        Vector2 start = startPoint.position;
        Vector2 dir = ((Vector2)endPoint.position - start).normalized;
        Vector2 end = start + dir * currentLength;
        Vector2 center = (start + end) / 2f;
        Vector2 size = new Vector2(currentLength, thickness);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, angle, targetLayer);

        foreach (var hit in hits)
        {
            IDamage damage = hit.GetComponent<IDamage>();
            if (damage != null && hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                damage.OnHitDamage(AtkDamage);
                this.gameObject.SetActive(false);
                if (PlayerMgr.instance.getPlayerType() == Player_Type.Paladin && !PlayerMgr.instance.getonPassive())
                {
                    PlayerMgr.instance.sumPassiveStack(1);
                }
            }
        }
    }

    private void offhit()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        if (!isActive) return;
        if (startPoint == null || endPoint == null) return;

        Vector2 start = startPoint.position;
        Vector2 dir = ((Vector2)endPoint.position - start).normalized;
        Vector2 end = start + dir * currentLength;
        Vector2 center = (start + end) / 2f;
        Vector2 size = new Vector2(currentLength, thickness);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Gizmos.color = Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }


}
