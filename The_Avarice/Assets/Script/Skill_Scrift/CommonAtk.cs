using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAtk : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(offRange());
    }

    IEnumerator offRange()
    {
        yield return new WaitForSeconds(0.03f);
        this.gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< Updated upstream
        IDamage damage = other.GetComponent<IDamage>(); //√Êµπ«— ø¿∫Í¡ß∆Æø°º≠ IDamage ¿Œ≈Õ∆‰¿ÃΩ∫∏¶ ∞°¡Æø≈
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // √Êµπ«— ø¿∫Í¡ß∆Æ∞° IDamage¿Œ≈Õ∆‰¿ÃΩ∫∏¶ ∞°¡ˆ∞Ì¿÷∞Ì ∑π¿ÃæÓ∞° enemy¿Ã∂Û∏È
        {
            damage.OnHitDamage(1f); //¿Œ≈Õ∆‰¿ÃΩ∫ø° º±æµ» OnHitDamage()∏ﬁº“µÂ∏¶ »£√‚ = ««∞›√≥∏Æ
=======
        IDamage damage = other.GetComponent<IDamage>(); //ÔøΩÊµπÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ∆ÆÔøΩÔøΩÔøΩÔøΩ IDamage ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÃΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ
        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // ÔøΩÊµπÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ∆ÆÔøΩÔøΩ IDamageÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÃΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ÷∞ÔøΩ ÔøΩÔøΩÔøΩÃæÓ∞° enemyÔøΩÃ∂ÔøΩÔøΩ
        {
            damage.OnHitDamage(1f); //ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩÃΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩ OnHitDamage()ÔøΩﬁº“µÂ∏¶ »£ÔøΩÔøΩ = ÔøΩ«∞ÔøΩ√≥ÔøΩÔøΩ
>>>>>>> Stashed changes
        }

    }
}
