using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Range(0f, 0.1f)] private float parallaxFactor = 0.1f;
    [SerializeField] private bool moveY = false;

    private float[] backgroundWidths;
    private float[] backgroundHeights;

    private int leftIndex = 0;
    private int rightIndex;
    private PixelPerfectCamera ppc;

    private Vector3 lastCameraPos;

    private void Awake()
    {
        rightIndex = backgrounds.Length - 1;

        Camera cam = Camera.main;
        if (cam != null)
        {
            ppc = cam.GetComponent<PixelPerfectCamera>();
            cameraTransform = cam.transform;
            lastCameraPos = cameraTransform.position;
        }

        backgroundWidths = new float[backgrounds.Length];
        backgroundHeights = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            SpriteRenderer sr = backgrounds[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                backgroundWidths[i] = sr.bounds.size.x - 0.05f;
                backgroundHeights[i] = sr.bounds.size.y - 0.05f;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        lastCameraPos = cameraTransform.position;


        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 moveAmount = new Vector3(
            deltaMovement.x * parallaxFactor,
            moveY ? deltaMovement.y * parallaxFactor : 0f,
            0f
            );

            Vector3 newPos = backgrounds[i].position + moveAmount;

            backgrounds[i].position = SnapToPixel(newPos);
        }

        // 카메라가 오른쪽 끝으로 넘어갈 경우
        if (cameraTransform.position.x >= backgrounds[rightIndex].position.x - (backgroundWidths[rightIndex] / 2f))
        {
            ScrollRight();

        }
        // 카메라가 왼쪽 끝으로 넘어갈 경우
        else if (cameraTransform.position.x <= backgrounds[leftIndex].position.x + (backgroundWidths[leftIndex] / 2f))
        {
            ScrollLeft();
        }
    }

    void ScrollRight()
    {
        Vector3 pos = new Vector3(
            backgrounds[rightIndex].position.x + backgroundWidths[rightIndex],
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
            backgrounds[leftIndex].position.x - backgroundWidths[rightIndex],
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
