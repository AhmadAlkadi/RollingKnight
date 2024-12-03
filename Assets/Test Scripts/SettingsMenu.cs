using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Scene currentScene;
    public NewPlayerMovement player;
    
    private bool activate = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    { 
        
        if (Input.GetKeyDown(KeyCode.Escape) && activate == true)
        {
            activate = false;
            player.enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && activate == false)
        {
            setMenuInactive();
        }
    }

    public void setMenuInactive()
    {
        activate = true;
        player.enabled = true;

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
    }
}
