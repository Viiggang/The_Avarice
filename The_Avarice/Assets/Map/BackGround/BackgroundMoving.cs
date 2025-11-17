using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class BackgroundMoving : MonoBehaviour
{
    void LatedUpdate()
    {
        gameObject.transform.localPosition = Camera.main.transform.localPosition;
    }
}
