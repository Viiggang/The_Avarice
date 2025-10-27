using DG.Tweening;
using System.Collections;
using System.IO;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEditor;
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

        // ĳ���� �߰��� �� ĳ���� �̸� �߰��� ��
        private static string[] characterList = {"Paladin", "Ignis",  "WindBreaker", "SoulEater"};
        // é�� �߰��� ������ ���� �ε��� �߰��� ��(File -> Build Settings -> Scene In Build)
        private int[] playableScenes = { 2, 3 };


        static SceneSettings()
        {
            var pathOfScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
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

        [MenuItem("Scene/Load Scene")]
        private static void SceneLoader()
        {
            SceneSettings window = GetWindow<SceneSettings>();
            tab = Tab.Load;
            labelText = "Load a Scene";
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
                    Close();
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
                    Close();
                    break;
                case Tab.Simulate:
                    for (int i = 0; i < characterList.Length; i++)
                    {
                        if (GUILayout.Button(characterList[i]))
                        {
                            LoadScene(characterList[i]);
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("Select Error");
                    Close();
                    return;
            }

            
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

        private static void SimulateScene(Player_Type player_Type)
        {
            PlayerMgr.instance.setPlayerType(player_Type);
            PlayerMgr.instance.Spawnplayer();
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

