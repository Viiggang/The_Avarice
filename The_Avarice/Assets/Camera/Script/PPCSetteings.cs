using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PixelPerfectCamera))]
public class PPCSetteings : MonoBehaviour
{
    [SerializeField]private PixelPerfectCamera ppc;
    private Vector2Int tempResolution;

    private void Awake()
    {
        tempResolution.x = Screen.width;
        tempResolution.y = Screen.height;
        UpdateResolution();

        SceneManager.sceneLoaded += SetPixelPerfectUnit;
    }

    private void Update()
    {
        if (Screen.width != tempResolution.x || Screen.height != tempResolution.y)
        {
            tempResolution.x = Screen.width;
            tempResolution.y = Screen.height;

            UpdateResolution();
        }
    }

    private void UpdateResolution()
    {
        if (ppc != null)
        {
            ppc.refResolutionX = Screen.width;
            ppc.refResolutionY = Screen.height;

            Debug.Log($"해상도 변경: {Screen.width} x {Screen.height}");
        }
        else
        {
            Debug.LogWarning("PixelPerfectCamera is not valid");
        }
    }

    private void SetPixelPerfectUnit(Scene scene, LoadSceneMode mode)
    {
        if(string.Compare(scene.name, "villageScene") == 0)
        {
            ppc.assetsPPU = 32;
        }
        else
        {
            ppc.assetsPPU = 16;
        }
    }
}
