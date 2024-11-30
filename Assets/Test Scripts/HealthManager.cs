using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    public int Health {
        get {return health;}
        set {health = Mathf.Clamp(value,0,3);}
    }

     private void OnEnable()
    {
        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the hearts array when the scene is loaded
        ReassignHeartReferences();
    }

    private void ReassignHeartReferences()
    {
        // Find the HeartsContainer and get all its child Image components
        GameObject heartsContainer = GameObject.Find("Canvas/HeartsContainer");
        if (heartsContainer != null)
        {
            hearts = heartsContainer.GetComponentsInChildren<Image>();
        }
        else
        {
            Debug.LogWarning("HeartsContainer not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}