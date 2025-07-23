using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Leein
{

    public class AudioMachine : Singleton<AudioMachine>
    {
        AudioSource m_Source;
        private Dictionary<AudioState, string> BgmPath;

        private void Awake()
        {
            base.Awake();
            m_Source = GetComponentInChildren<AudioSource>();

            BgmPath = new Dictionary<AudioState, string>
            {
                {AudioState.TitleMap,"Audio_resources/hebi"},
                {AudioState.CharatorSelect,"Audio_resources/eve"},
            };


        }

        public void ChangeAudioClip(AudioState AudioPath)
        {

            if (BgmPath.TryGetValue(AudioPath, out string path))
            {
                var Clip = Resources.Load<AudioClip>(path);
                m_Source.clip = Clip;
                m_Source.loop = true;

                m_Source.Play();
            }


        }

    }

}
