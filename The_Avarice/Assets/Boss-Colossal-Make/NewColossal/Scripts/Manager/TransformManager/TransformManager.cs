using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public Transform Boss;
    public Transform Player;
    public int targetLayer = 6;
    private void Awake()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();


        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == targetLayer)
            {

               Player = obj.transform;
                Debug.Log("플레이어 찾음");
               
            }
        }


    }
}
