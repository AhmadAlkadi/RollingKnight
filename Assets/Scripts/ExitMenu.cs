using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : MonoBehaviour
{
    private bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                print("quit");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SettingMenu"))
        {
            GetComponent<Text>().color = Color.red;
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SettingMenu"))
        {
            GetComponent<Text>().color = Color.black;
            active = false;
        }
    }
}
