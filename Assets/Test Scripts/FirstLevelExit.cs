using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelExit : MonoBehaviour
{
    public string loadScene = "fire_test";
    private bool checker;

    private void OnTriggerEnter2D(Collider2D other)
    {
        checker = true;   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checker = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && checker)
        {
            SceneManager.LoadScene(loadScene); 
        }
    }
}
