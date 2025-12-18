using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject enlargeMap;

    private KeyCode toggleMapKey = KeyCode.M;

    private void Awake()
    {
        miniMap.gameObject.SetActive(true);
        enlargeMap.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleMapKey))
            ToggleMap();
    }

    private void ToggleMap()
    {
        bool isMiniMapActive = miniMap.gameObject.activeSelf;

        miniMap.gameObject.SetActive(!isMiniMapActive);
        enlargeMap.gameObject.SetActive(isMiniMapActive);
    }
}