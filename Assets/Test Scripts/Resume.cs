using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    private bool active = false;
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
                SettingsMenu parent = this.transform.parent.gameObject.GetComponent<SettingsMenu>();
                parent.setMenuInactive();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("SettingMenu"))
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
