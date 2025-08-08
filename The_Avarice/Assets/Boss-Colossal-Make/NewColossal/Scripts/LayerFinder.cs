using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFinder : MonoBehaviour
{
    public int targetLayer = 11; // 인스펙트창에서 플레이어 Layer번호 찾아서 수정 기록해야함

    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
       

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == targetLayer)
            {
                ColossalHandler.Instance.targetPlayer = obj.transform;
            }
        }

       
    }
}
