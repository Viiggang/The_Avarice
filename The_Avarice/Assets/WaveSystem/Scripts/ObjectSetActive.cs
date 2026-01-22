using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetActive : MonoBehaviour
{
    [SerializeField]private GameObject go;
    public void off()
    {
        go.SetActive(false);
    }
}
