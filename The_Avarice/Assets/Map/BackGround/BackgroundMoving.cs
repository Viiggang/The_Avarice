using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class BackgroundMoving : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 localPosition = Camera.main.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0); 
    }

    
}
