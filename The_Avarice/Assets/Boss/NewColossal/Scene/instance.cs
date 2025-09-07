using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    private void Awake()
    {
        Instantiate(prefab,new Vector3(5,1,0),Quaternion.identity);
    }
}
