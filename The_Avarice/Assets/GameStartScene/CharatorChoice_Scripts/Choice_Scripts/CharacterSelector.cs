using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharacterSelect
{
    public class CharacterSelector :Singleton<CharacterSelector>
    {
        public Player_Type player_Type;
        private const string SceneName= "SampleScene";
        private void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            player_Type = Player_Type.NULL;
        }
        public void SetPaladin()
        {
            player_Type = Player_Type.Paladin;
            NextScene();
        }

        public void SetWindBreaker()
        {
            player_Type = Player_Type.WindBreaker;
            NextScene();
        }

        public void SetIgnis()
        {
            player_Type = Player_Type.Ignis;
            NextScene();
        }

        public void SetSoulEater()
        {
            player_Type = Player_Type.SoulEater;
            NextScene();
        }
        public void NextScene()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
   
}

