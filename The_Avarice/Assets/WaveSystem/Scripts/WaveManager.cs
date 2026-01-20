using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]private WaveStage m_waveStage;

    [SerializeField] GameObject[] WavePosition;

    [SerializeField] private Dictionary<WaveStage, Action> sumon ;
    

    private void Awake()
    {
        sumon = new Dictionary<WaveStage, Action>()
        {
            { WaveStage.Stage1,Wave1},
         { WaveStage.Stage2,Wave1},
          { WaveStage.Stage3,Wave1},
           { WaveStage.End,Wave1},
        };
    }
    public WaveStage waveStage
    {
        get =>m_waveStage;
        set
        {
             
            m_waveStage++;
            sumon[value]?.Invoke();
         
        }
    }

    public void Wave1()
    {
        WavePosition[2].SetActive(true);
        WavePosition[3].SetActive(true);
        WavePosition[6].SetActive(true);
        WavePosition[7].SetActive(true);
    }

    [ContextMenu("Start Wave")]
    public void Up()
    {
        waveStage+=1;
    }
}
