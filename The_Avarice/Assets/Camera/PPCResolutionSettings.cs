using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(PixelPerfectCamera))]
public class PPCResolutionSettings : MonoBehaviour
{
    private PixelPerfectCamera ppc;
    private Vector2Int tempResolution;

    [SerializeField] private float tolerance = 0.01f; //오차 범위
    [SerializeField] private float ppu = 32;

    void Awake()
    {
        ppc = gameObject.GetComponent<PixelPerfectCamera>();

        tempResolution.x = Screen.width;
        tempResolution.y = Screen.height;
        UpdateResolution();
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

    private void LateUpdate()
    {
        RoundToPixel();
    }
    private void UpdateResolution()
    {
        ppc.refResolutionX = Screen.width / 10;
        ppc.refResolutionY = Screen.height / 10;

        Debug.Log($"해상도 변경: {Screen.width} x {Screen.height}");
    }

    private void RoundToPixel()
    {
        float pixelsPerUnit = (float)Screen.height / (ppc.refResolutionY / (float)ppu) / 10;

        float roundedPPU = Mathf.Round(pixelsPerUnit);
        if (Mathf.Abs(pixelsPerUnit - roundedPPU) <= tolerance)
        {
            pixelsPerUnit = roundedPPU;
        }
        
        ppc.assetsPPU = Mathf.RoundToInt(pixelsPerUnit);
    }
}
