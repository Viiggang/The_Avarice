using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]private WaveStage m_waveStage;

    [SerializeField] GameObject[] WavePosition1;
    [SerializeField] GameObject[] WavePosition2;
    [SerializeField] GameObject[] WavePosition3;

    [SerializeField] private Dictionary<WaveStage, Action> sumon ;
    [SerializeField] private int m_livingCount = 0;
    public int livingCount
    {
        get => m_livingCount;
        set
        {
            m_livingCount = value;
            if(m_livingCount ==0)
            {
                Up();
            }
        }
    }
    private void Awake()
    {
        sumon = new Dictionary<WaveStage, Action>()
        {
            { WaveStage.Stage1,Wave1},
         { WaveStage.Stage2,Wave2},
          { WaveStage.Stage3,Wave3},
           { WaveStage.End,WaveEnd},
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
        foreach (var item in WavePosition1)
        {
            item.SetActive(true);
        }
    }
    public void Wave2()
    {
        foreach (var item in WavePosition2)
        {
            item.SetActive(true);
        }
    }
    public void Wave3()
    {
        foreach (var item in WavePosition3)
        {
            item.SetActive(true);
        }
    }

    [ContextMenu("Start Wave")]
    public void Up()
    {
        waveStage+=1;
    }

    public void WaveEnd()
    {

    }

    public   void countUp()
    {
        livingCount += 1;
    }
    public void CountDown()
    {
        livingCount -= 1;

    }
}
