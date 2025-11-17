
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneTester
    : EditorWindow
{
#if UNITY_EDITOR
    private enum Tab { Open, Load, Simulate}
    private static Tab tab;

    private static string labelText = default;

    // 캐릭터 추가될 때 캐릭터 이름 추가할 것
    private static string[] characterList = { "Paladin", "Ignis", "WindBreaker", "SoulEater" };
    // 챕터 추가될 때마다 씬의 인덱스 추가할 것(File -> Build Settings -> Scene In Build)
    private static int[] playableScenes = { 2, 3 };
    private static bool next = false;

    static int SelectedSceneIndex = -1;

    [MenuItem("Scene/Open Scene")]
    private static void SceneOpener()
    {
        SceneTester window = GetWindow<SceneTester>();
        tab = Tab.Open;
        labelText = "Choose a Scene";
        window.titleContent = new GUIContent("Scene Opener");
        window.Show();
    }

    [MenuItem("Scene/Change Scene")]
    private static void SceneChanger()
    {
        SceneTester window = GetWindow<SceneTester>();
        tab = Tab.Load;
        labelText = "Change a Scene on PlayMode";
        window.titleContent = new GUIContent("Scene Loader");
        window.Show();
    }

    [MenuItem("Scene/Simulate Scene")]
    private static void SceneSimulator()
    {
        SceneTester window = GetWindow<SceneTester>();
        tab = Tab.Simulate;
        labelText = "Choose a Character";
        window.titleContent = new GUIContent("Scene Simulator");
        window.Show();
    }

    [MenuItem("Scene/Reset StartingScene")]
    private static void StartingScene()
    {
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
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
                for (int i = 0; i < playableScenes.Length; i++)
                {
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
                        int sceneIndex = i;
                        var scene = EditorBuildSettings.scenes[playableScenes[i]];
                        if (scene.enabled)
                        {
                            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                            if (GUILayout.Button(sceneName))
                            {
                                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                                {
                                    // 플레이 모드 상태 체크
                                    EditorApplication.playModeStateChanged += OnPlayModeChanged;
                                    SelectedSceneIndex = sceneIndex;
                                    EditorApplication.delayCall += () =>
                                    {
                                        // Play 모드 시작
                                        EditorApplication.isPlaying = true;
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
            if (PlayerMgr.instance.getPlayerType() != Player_Type.NULL)
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
        // 플레이가 되기 바로 직전에 호출됨
        if (state == PlayModeStateChange.ExitingEditMode && SelectedSceneIndex >= 0)
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[playableScenes[SelectedSceneIndex]].path);
            SelectedSceneIndex = -1; // 초기화
            next = false;
        }

        // 플레이 모드로 진입하면서 실행됨
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            PlayerMgr.instance.Spawnplayer();
        }

        if(state == PlayModeStateChange.EnteredEditMode)
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            return;
        }
    }
#else
        static SceneSettings()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }   
#endif
}
