using UnityEngine;
using UnityEngine.Rendering;

public class SettingMenuMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.SyncTransforms();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 236.9f, gameObject.transform.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 187.8f, gameObject.transform.localPosition.z);
        }
    }
}
