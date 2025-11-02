using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    [SerializeField] public UpdateUI questUI;
    private void Awake()
    {
        questUI.init(textMeshPro);
    }
}
