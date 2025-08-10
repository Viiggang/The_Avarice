using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAudio : MonoBehaviour
{
   [Leein.InspectorName("����� �뷡 ������ ��")] [SerializeField]private AudioClip audioClip;
   [Leein.InspectorName("����� �ҽ� �ִ� ��")][SerializeField]private AudioSource audioSource;
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
            Debug.Log("hasAudioClip ����");
        }
    }
}
