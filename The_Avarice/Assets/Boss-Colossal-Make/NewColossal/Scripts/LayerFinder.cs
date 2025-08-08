using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFinder : MonoBehaviour
{
    public int targetLayer = 11; // �ν���Ʈâ���� �÷��̾� Layer��ȣ ã�Ƽ� ���� ����ؾ���

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
