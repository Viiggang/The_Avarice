using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_LightCut : MonoBehaviour
{
    [Header("Hitbox Points")]
<<<<<<< Updated upstream
    public Transform startPoint;   // ¾Ö´Ï¸ÞÀÌ¼Ç¿¡¼­ ½ÃÀÛ À§Ä¡
    public Transform endPoint;     // ¾Ö´Ï¸ÞÀÌ¼Ç¿¡¼­ ³¡ À§Ä¡

    [Header("Hitbox Settings")]
    public float thickness = 0.2f; // ÆÇÁ¤ µÎ²²
    public LayerMask targetLayer;  // Å¸°Ý ´ë»ó ·¹ÀÌ¾î
    public float activeTime = 0.05f; // ÆÇÁ¤ À¯Áö ½Ã°£ (ÃÊ)

    public Animator animator;
    public string attackStateName = "Pal_LightCut"; // °ø°Ý ¾Ö´Ï¸ÞÀÌ¼Ç »óÅÂ¸í

    public float minLength = 0.1f;  // ÃÖ¼Ò Ãæµ¹ ±æÀÌ
    public float maxLength = 2f;    // ÃÖ´ë Ãæµ¹ ±æÀÌ
=======
    public Transform startPoint;   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼Ç¿ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡
    public Transform endPoint;     // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼Ç¿ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½Ä¡

    [Header("Hitbox Settings")]
    public float thickness = 0.2f; // ï¿½ï¿½ï¿½ï¿½ ï¿½Î²ï¿½
    public LayerMask targetLayer;  // Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ì¾ï¿½
    public float activeTime = 0.05f; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ã°ï¿½ (ï¿½ï¿½)

    public Animator animator;
    public string attackStateName = "Pal_LightCut"; // ï¿½ï¿½ï¿½ï¿½ ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½Â¸ï¿½

    public float minLength = 0.1f;  // ï¿½Ö¼ï¿½ ï¿½æµ¹ ï¿½ï¿½ï¿½ï¿½
    public float maxLength = 2f;    // ï¿½Ö´ï¿½ ï¿½æµ¹ ï¿½ï¿½ï¿½ï¿½
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            activeTime = clipInfo[0].clip.length;  // ¾Ö´Ï¸ÞÀÌ¼Ç ÀüÃ¼ ±æÀÌ·Î ¼¼ÆÃ
=======
            activeTime = clipInfo[0].clip.length;  // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½Ã¼ ï¿½ï¿½ï¿½Ì·ï¿½ ï¿½ï¿½ï¿½ï¿½
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        // ¾Ö´Ï¸ÞÀÌ¼Ç ÁøÇà·ü¿¡ speedMultiplier Àû¿ë ÈÄ 0~1·Î Å¬·¥ÇÁ
=======
        // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ speedMultiplier ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ 0~1ï¿½ï¿½ Å¬ï¿½ï¿½ï¿½ï¿½
>>>>>>> Stashed changes
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
                damage.OnHitDamage(1f);
                this.gameObject.SetActive(false);
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
