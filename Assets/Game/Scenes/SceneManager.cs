using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager :MonoBehaviour
{


    private int _currentSceneIndex;

    public int CurrentSceneIndex { get { return _currentSceneIndex; } }

    //Singleton Pattern

    public static LevelManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitBeforeSceneLoad()
    {
        {
            if(Instance == null)
            {
                GameObject singletonObject = new GameObject("LevelManager");
                Instance = singletonObject.AddComponent<LevelManager>();
                DontDestroyOnLoad(singletonObject);
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _currentSceneIndex = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene("StartGame", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private void Start()
    {
        string name = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1).name;
        int count = UnityEngine.SceneManagement.SceneManager.loadedSceneCount;
        LoadNextScene();
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }


    public bool LoadNextScene()
    {
        string name = "Level" + (_currentSceneIndex+1);
        if (SceneUtility.GetBuildIndexByScenePath(name) == -1) return false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(name, UnityEngine.SceneManagement.LoadSceneMode.Single);
        _currentSceneIndex++;
        return true;
    }
}
