using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NextSceneTrigger : MonoBehaviour
{
    public string targetSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneLoader.Instance.ChangeScene(targetSceneName);
        }
    }
}
