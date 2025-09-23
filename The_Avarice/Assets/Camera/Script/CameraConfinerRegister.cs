using UnityEngine;

public class CameraConfinerRegister : MonoBehaviour
{
    private void Start()
    {
        var confiner = this.gameObject.GetComponent<PolygonCollider2D>();
        CameraManager.Instance.cameraConfiner = confiner;
    }
}