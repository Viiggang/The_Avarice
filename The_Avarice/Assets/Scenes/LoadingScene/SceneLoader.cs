using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    [InitializeOnLoad]
    public class SceneSettings : EditorWindow
    {
        private enum Tab {Open, Load, Simulate }
        private static Tab tab;

        private static string labelText = default;

        // 캐릭터 추가될 때 캐릭터 이름 추가할 것
        private static string[] characterList = {"Paladin", "Ignis",  "WindBreaker", "SoulEater"};
        // 챕터 추가될 때마다 씬의 인덱스 추가할 것(File -> Build Settings -> Scene In Build)
        private static int[] playableScenes = { 2, 3 };
        private static bool next = false;

        static SceneSettings()
        {
            var pathOfScene = EditorBuildSettings.scenes[0].path;
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfScene);
            EditorSceneManager.playModeStartScene = scene;
        
        }

        [MenuItem("Scene/Open Scene")]
        private static void SceneOpener()
        {
            SceneSettings window = GetWindow<SceneSettings>();
            tab = Tab.Open;
            labelText = "Open a Scene";
            window.titleContent = new GUIContent("Scene Opener");
            window.Show();
        }

        [MenuItem("Scene/Change Scene")]
        private static void SceneChanger()
        {
            SceneSettings window = GetWindow<SceneSettings>();
            tab = Tab.Load;
            labelText = "Change a Scene on PlayMode";
            window.titleContent = new GUIContent("Scene Loader");
            window.Show();
        }

        [MenuItem("Scene/Simulate Scene")]
        private static void SceneSimulator()
        {
            SceneSettings window = GetWindow<SceneSettings>();
            tab = Tab.Simulate;
            labelText = "Selete a Character";
            window.titleContent = new GUIContent("Scene Simulator");
            window.Show();
        }


        private void OnGUI()
        {
            GUILayout.Label(labelText, EditorStyles.boldLabel);

            switch (tab)
            {
                case Tab.Open:
                    foreach (var scene in EditorBuildSettings.scenes)
                    {
                        if (scene.enabled && scene.path != EditorBuildSettings.scenes[4].path)
                        {
                            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                            if (GUILayout.Button(sceneName))
                            {
                                OpenScene(scene.path);
                            }
                        }
                    }
                    break;
                case Tab.Load:
                    for(int i = 0; i < playableScenes.Length; i++) {
                        var scene = EditorBuildSettings.scenes[playableScenes[i]];
                        if (scene.enabled)
                        {
                            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                            if (GUILayout.Button(sceneName))
                            {
                                LoadScene(sceneName);
                            }
                        }
                    }
                    break;
                case Tab.Simulate:
                    if (!next)
                    {
                        for (int i = 0; i < characterList.Length; i++)
                        {
                            if (GUILayout.Button(characterList[i]))
                            {
                                CharacterChoice(characterList[i]);
                            }
                        }
                    }
                    else
                    {
                        GUILayout.Label("Choose a Scene", EditorStyles.boldLabel);

                        for (int i = 0; i < playableScenes.Length; i++)
                        {
                            var scene = EditorBuildSettings.scenes[playableScenes[i]];
                            if (scene.enabled)
                            {
                                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                                if (GUILayout.Button(sceneName))
                                {
                                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                                    {
                                        // Play 모드 시작
                                        EditorApplication.playModeStateChanged += OnPlayModeChanged;
                                        EditorApplication.isPlaying = true;
                                        EditorApplication.delayCall += () =>
                                        {
                                            temppath = sceneName;
                                            SceneManager.LoadScene(sceneName);
                                            PlayerMgr.instance.Spawnplayer();
                                            next = false;
                                            this.Close();
                                        };
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
           // Close();

            
        }

        private static void OpenScene(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        private static void LoadScene(string sceneName)
        {
            if (Application.isPlaying)
            {
                if(PlayerMgr.instance.getPlayerType() != Player_Type.NULL)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Debug.LogError("Use this tool on Playable Scene");
                }
            }
            else
            {
                Debug.LogWarning("Please use this tool only RunTime");
            }
        }

        private static void CharacterChoice(string characterName)
        {
            switch (characterName)
            {
                case "Paladin":
                    PlayerMgr.instance.setPlayerType(Player_Type.Paladin);
                    break;
                case "Ignis":
                    PlayerMgr.instance.setPlayerType(Player_Type.Ignis);
                    break;
                case "WindBreaker":
                    PlayerMgr.instance.setPlayerType(Player_Type.WindBreaker);
                    break;
                case "SoulEater":
                    PlayerMgr.instance.setPlayerType(Player_Type.SoulEater);
                    break;
            }
            next = true;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            }
        }
    }

    public string nextSceneName { get; private set; }

    public static SceneLoader Instance { get {  return _instance; } }
    private static SceneLoader _instance;

    public GameObject fadeImage;
    public GameObject loadingCharacter;
    public GameObject loadingText;

    public float fadeDuration = 1f;

    private Image image;

    private void Awake()
    {
        if(_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        image = fadeImage.GetComponent<Image>();

    }

    public void ChangeScene(string sceneName)
    {
        fadeImage.SetActive(true);

        nextSceneName = sceneName;

        image.DOFade(1, fadeDuration).OnStart(() =>
        {
            image.raycastTarget = true;
        })
            .OnComplete(() =>
        {
            SceneManager.LoadScene("LoadingScene");
            StartCoroutine("LoadScene");
        });
    }

    public IEnumerator LoadScene()
    {
        loadingCharacter.SetActive(true);
        loadingText.SetActive(true);

        Player_Type type = PlayerMgr.instance.getPlayerType();
        Animator animator = loadingCharacter.GetComponent<Animator>();
        Text text = loadingText.GetComponent<Text>();

        AsyncOperation loadscene = SceneManager.LoadSceneAsync(nextSceneName);
        loadscene.allowSceneActivation = false;

        float loadingtime = Time.time;

        float past_time = 0;
        float percentage = 0;

        animator.SetInteger("type", (int)PlayerMgr.instance.getPlayerType());

        while (true)
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100 && Time.time - loadingtime >= 1.5f)
                {
                    loadscene.allowSceneActivation = true;
                    break;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, loadscene.progress * 100f, past_time);
                if (percentage >= 90)
                {
                    past_time = 0;
                }
            }
            text.text = percentage.ToString("0") + "%";
        }

        CompleteSceneLoaded();
    }

    private void CompleteSceneLoaded()
    {
        image.DOFade(0, fadeDuration)
        .OnStart(() => {
            loadingCharacter.SetActive(false);
            loadingText.SetActive(false);
        })
        .OnComplete(() => {
            fadeImage.SetActive(false);
            image.raycastTarget = false;
        });
    }

    //public IEnumerator FadeOut()
    //{
    //    fadeImage.raycastTarget = true;
    //    Color color = fadeImage.color;
    //    for (float t = 0; t < fadeDuration; t += Time.deltaTime)
    //    {
    //        color.a = Mathf.Lerp(0, 1, t / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }
    //    color.a = 1;
    //    fadeImage.color = color;
    //}

    //public IEnumerator FadeIn()
    //{
    //    Color color = fadeImage.color;
    //    for (float t = 0; t < fadeDuration; t += Time.deltaTime)
    //    {
    //        color.a = Mathf.Lerp(1, 0, t / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }
    //    color.a = 0;
    //    fadeImage.color = color;
    //    fadeImage.raycastTarget = false;
    //}
}

