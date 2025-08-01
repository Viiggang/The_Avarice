using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceChartor : MonoBehaviour
{
    public character m_character;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetChartorChoiceInfo);
    }
    void SetChartorChoiceInfo()
    {
        CharacterSelectionController.Instance.CharacterSelection = m_character;
    }
}
