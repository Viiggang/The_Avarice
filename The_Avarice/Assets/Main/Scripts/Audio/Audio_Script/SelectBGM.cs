using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Leein
{

    public class SelectBGM : MonoBehaviour
    {

        public AudioState Audiopath;
        protected void Start()
        {
            if (AudioMachine.Instance != null)
            {
                AudioMachine.Instance.ChangeAudioClip(Audiopath);
            }
        }
    }

}
