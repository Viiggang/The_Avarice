using System.Collections.Generic;
using UnityEngine;

public class Ign_SpellCast : MonoBehaviour
{
    [Header("Ray Settings")]
    public float rayDistance = 5f;
    public LayerMask targetLayer;

    [Header("Offsets")]
    public Vector2 fireOffset = Vector2.zero; 
    public Vector2 hitOffset = Vector2.zero;   

    [Header("Prefab Pool Settings")]
    public GameObject prefab;
    public int poolSize = 10;

    private List<GameObject> prefabPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            prefabPool.Add(obj);
        }
    }

    private Vector2 CalculateRayOrigin(float directionSign)
    {

        Vector2 finalOffset = new Vector2(fireOffset.x * directionSign, fireOffset.y);
        return (Vector2)transform.position + finalOffset;
    }

    private bool TryGetRayHitPoint(float directionSign, out Vector2 hitPos)
    {
        hitPos = Vector2.zero;

        Vector2 origin = CalculateRayOrigin(directionSign);
        Vector2 dir = new Vector2(directionSign, 0);

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, targetLayer);

        if (hit.collider != null)
        {
            hitPos = hit.point;
            return true;
        }

        return false;
    }


    public void FireRay() //Anim Event
    {
        float directionSign = PlayerMgr.instance.Direction ? 1f : -1f;

        Vector2 origin = CalculateRayOrigin(directionSign);
        Vector2 rayDir = new Vector2(directionSign, 0);

        Vector2 spawnPos;

        if (PlayerMgr.instance.ElementType == Element_Type.Thunder)
        {
            spawnPos = origin + (rayDir * rayDistance) * 0.5f;
            spawnPos.y += 0.09f;
        }
        else
        {
            if (TryGetRayHitPoint(directionSign, out Vector2 hitPoint))
            {
                Vector2 hitOffsetFinal = new Vector2(hitOffset.x * directionSign, hitOffset.y);
                spawnPos = hitPoint + hitOffsetFinal;
            }
            else
            {
                spawnPos = origin + rayDir * rayDistance;
            }
        }

        GameObject effect = GetInactivePrefabFromPool();
        if (effect != null)
        {
            effect.transform.position = spawnPos;

            effect.transform.localScale = new Vector3(directionSign, 1f, 1f);

            effect.SetActive(true);
        }
    }


    private GameObject GetInactivePrefabFromPool()
    {
        foreach (var obj in prefabPool)
        {
            if (!obj.activeSelf)
                return obj;
        }

        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        prefabPool.Add(newObj);
        return newObj;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;

        float directionSign = PlayerMgr.instance.Direction ? 1f : -1f;

        Vector2 origin = CalculateRayOrigin(directionSign);
        Vector2 dir = new Vector2(directionSign, 0);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + dir * rayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(origin + dir * rayDistance, 0.1f);
    }
}
