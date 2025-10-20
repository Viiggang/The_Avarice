using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PPCSetteings : MonoBehaviour
{
    private PixelPerfectCamera ppc;
    private Vector2Int tempResolution;

    private void Start()
    {
        tempResolution.x = Screen.width;
        tempResolution.y = Screen.height;

        Camera.main.gameObject.AddComponent<PixelPerfectCamera>();
        ppc = Camera.main.GetComponent<PixelPerfectCamera>() ?? null;

        if(ppc == null )
        {
            Debug.LogError("Pixel perfect camera isn't exist");
            return;
        }    

        ppc.gridSnapping = PixelPerfectCamera.GridSnapping.PixelSnapping;
        ppc.cropFrame = PixelPerfectCamera.CropFrame.Letterbox;

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
            CameraManager.Instance.SetLensSize();
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
