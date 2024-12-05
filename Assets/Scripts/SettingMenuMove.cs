using UnityEngine;
using UnityEngine.Rendering;

public class SettingMenuMove : MonoBehaviour
{
    float move = 238.9338f;
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
            if (move <= 238.9338f)
            {
                move += 51.0f;
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, move, gameObject.transform.localPosition.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (move >= 144.0f)
            {
                move -= 51.0f;
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, move, gameObject.transform.localPosition.z);
            }
        }
    }
}
