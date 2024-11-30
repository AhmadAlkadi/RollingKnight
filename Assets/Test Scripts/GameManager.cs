using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HealthManager healthManager;
    public bool ignoreNewGameStart = false;
    public bool ignoreGameOver = false;

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        //Access to HealthManager to get current player's health
        healthManager = GetComponent<HealthManager>();

        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
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
        LoadLevel(1, 1);
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

    public void ResetLevel()
    {
        if (healthManager.health > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }



    

    
}
