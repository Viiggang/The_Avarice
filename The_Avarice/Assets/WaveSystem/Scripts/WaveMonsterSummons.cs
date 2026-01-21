using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMonstersummons : MonoBehaviour
{
   
    [SerializeField]private GameObject[] SummonsMonster;
    [SerializeField] private WaveManager WaveManager;
    public void Summon()
    {
        WaveManager.countUp();
        if (WaveManager.waveStage ==WaveStage.Stage1)
        {
            
            var Object=Instantiate(SummonsMonster[0], this.transform.position, Quaternion.identity);
            var com=Object.GetComponentInChildren<MonsterStatus>();
            com.OnDead += WaveManager.CountDown;
        }
        else if (WaveManager.waveStage == WaveStage.Stage2)
        {
            var Object = Instantiate(SummonsMonster[1], this.transform.position, Quaternion.identity);
            var com = Object.GetComponentInChildren<MonsterStatus>();
            com.OnDead += WaveManager.CountDown;
        }
        else if (WaveManager.waveStage == WaveStage.Stage3)
        {
            var Object = Instantiate(SummonsMonster[2], this.transform.position, Quaternion.identity);
            var com = Object.GetComponentInChildren<MonsterStatus>();
            com.OnDead += WaveManager.CountDown;
        }
        
    }
}
