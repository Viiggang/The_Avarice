using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWaveSystem : MonoBehaviour
{
    [SerializeField] private WaveManager WaveManager;
    private void OnEnable()
    {
        WaveManager?.Up();
    }
}
