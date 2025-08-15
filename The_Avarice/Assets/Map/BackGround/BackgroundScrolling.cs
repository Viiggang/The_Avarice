using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float backgroundWidth;
    [SerializeField] private Transform cameraTransform;

    private int leftIndex = 0;
    private int rightIndex;
    private PixelPerfectCamera ppc;

    private void Awake()
    {
        rightIndex = backgrounds.Length - 1;

        Camera cam = Camera.main;
        if (cam != null)
        {
            ppc = cam.GetComponent<PixelPerfectCamera>();
        }
    }
    private void Update()
    {
        // 카메라가 오른쪽 끝으로 넘어갈 경우
        if (cameraTransform.position.x >= backgrounds[rightIndex].position.x - (backgroundWidth / 2f))
        {
            ScrollRight();

        }
        // 카메라가 왼쪽 끝으로 넘어갈 경우
        else if (cameraTransform.position.x <= backgrounds[leftIndex].position.x + (backgroundWidth / 2f))
        {
            ScrollLeft();
        }
    }

    void ScrollRight()
    {
        Vector3 pos = new Vector3(
            backgrounds[rightIndex].position.x + backgroundWidth,
            backgrounds[leftIndex].position.y,
            backgrounds[leftIndex].position.z
        );

        backgrounds[leftIndex].position = SnapToPixel(pos);

        rightIndex = leftIndex;
        leftIndex = (leftIndex + 1) % backgrounds.Length;
    }

    void ScrollLeft()
    {
        Vector3 pos = new Vector3(
            backgrounds[leftIndex].position.x - backgroundWidth,
            backgrounds[rightIndex].position.y,
            backgrounds[rightIndex].position.z
        );

        backgrounds[rightIndex].position = SnapToPixel(pos);

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = backgrounds.Length - 1;
    }

    Vector3 SnapToPixel(Vector3 pos)
    {
        if (ppc == null || ppc.assetsPPU <= 0)
            return pos;

        float unitsPerPixel = 1f / ppc.assetsPPU;
        pos.x = Mathf.Round(pos.x / unitsPerPixel) * unitsPerPixel;
        pos.y = Mathf.Round(pos.y / unitsPerPixel) * unitsPerPixel;
        return pos;
    }
}
