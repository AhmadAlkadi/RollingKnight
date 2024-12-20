using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get
        {
            if (_instance is null)
            {
                Debug.LogWarning("Game manager is NULL!");
            }

            return _instance;
        }

        private set {} }

    private HealthManager healthManager;
    public bool ignoreNewGameStart = false;
    public bool ignoreGameOver = false;

    public string sceneLoadName;

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        //Access to HealthManager to get current player's health
        healthManager = GetComponent<HealthManager>();

        if (_instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        if (!ignoreNewGameStart)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        healthManager.health = 3;
        LoadLevel(sceneLoadName);
    }

    public void GameOver()
    {
        if (!ignoreGameOver)
        {
            NewGame();
        }
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void LoadLevel(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void ResetLevel()
    {
        if (healthManager.health > 0) {
            LoadLevel(sceneLoadName);
        } else {
            GameOver();
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }



    

    
}
