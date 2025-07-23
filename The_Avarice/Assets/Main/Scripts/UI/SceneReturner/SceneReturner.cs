using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneReturner : MonoBehaviour
{
    Button button;
    string scene = "GameStartScreen";
    private void Awake()
    {
        button = GetComponent<Button>();    

    }
    private void Start()
    {
        button.onClick.AddListener(backScene);
    }
    void backScene()
    {
        SceneManager.LoadScene(scene);
    }
}
