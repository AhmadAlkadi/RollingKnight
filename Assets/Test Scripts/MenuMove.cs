using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuMove : MonoBehaviour
{
    public float step = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (gameObject.transform.localPosition.y + step <= -4.0f)
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + step, gameObject.transform.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gameObject.transform.localPosition.y + step >= -7.92f)
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - step, gameObject.transform.localPosition.z);
        }
    }
}
