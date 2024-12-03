using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToMainMenu : MonoBehaviour
{
    private bool active = false;
    public string loadSceneName = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.SyncTransforms();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(loadSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SettingMenu"))
        {
            this.transform.GetChild(0).GetComponent<Text>().color = Color.red;
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SettingMenu"))
        {
            this.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            active = false;
        }
    }
}
