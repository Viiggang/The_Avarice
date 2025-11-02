using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestUpddate : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] public UpdateText Data;

    private void Awake()
    {
        
        Data =Instantiate(Data);
    }
}
