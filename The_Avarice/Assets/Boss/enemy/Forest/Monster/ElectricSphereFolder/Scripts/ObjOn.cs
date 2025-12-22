using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOn : MonoBehaviour
{
    public GameObject obj;
    private void Start()
    {
        StartCoroutine(OnObj());
    }
    IEnumerator OnObj()
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);
    }
}
