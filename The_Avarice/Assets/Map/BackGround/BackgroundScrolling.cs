using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public Transform[] backgrounds;
    public float backgroundWidth;
    public Transform cameraTransform;

    private int leftIndex = 0;
    private int rightIndex;

    private void Start()
    {
        rightIndex = backgrounds.Length - 1;
    }

    private void Update()
    {
        // 카메라가 오른쪽 끝으로 넘어갈 경우
        if (cameraTransform.position.x > backgrounds[rightIndex].position.x - (backgroundWidth / 2))
        {
            ScrollRight();
        }
        // 카메라가 왼쪽 끝으로 넘어갈 경우
        else if (cameraTransform.position.x < backgrounds[leftIndex].position.x + (backgroundWidth / 2))
        {
            ScrollLeft();
        }
    }

    void ScrollRight()
    {
        backgrounds[leftIndex].position = new Vector3(
            backgrounds[rightIndex].position.x + backgroundWidth,
            backgrounds[leftIndex].position.y,
            backgrounds[leftIndex].position.z
        );

        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex >= backgrounds.Length)
            leftIndex = 0;
    }

    void ScrollLeft()
    {
        backgrounds[rightIndex].position = new Vector3(
            backgrounds[leftIndex].position.x - backgroundWidth,
            backgrounds[rightIndex].position.y,
            backgrounds[rightIndex].position.z
        );

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = backgrounds.Length - 1;
    }
}
