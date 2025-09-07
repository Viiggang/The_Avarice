using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAudio : MonoBehaviour
{
   [Leein.InspectorName("사용할 노래 넣으면 됨")] [SerializeField]private AudioClip audioClip;
   [Leein.InspectorName("오디오 소스 넣는 곳")][SerializeField]private AudioSource audioSource;
   private bool hasAudioClip;
    // Start is called before the first frame update
   private void Start()
    {
         InitAudio();
    }
    //1038
    private void  InitAudio()
    {
        audioSource.clip = audioClip;

        hasAudioClip = audioSource.clip;
        if (hasAudioClip)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("hasAudioClip 없음");
        }
    }
}
