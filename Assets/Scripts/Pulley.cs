/**
 * 
 * example from:
 * https://www.youtube.com/watch?v=yQiR2-0sbNw
 * https://www.youtube.com/watch?app=desktop&v=s3jHsFA06Oo&t=0s
 * 
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class Pulley : MonoBehaviour
{
    public GameObject mouseHoverObject;
    public ChainPulley pulleyObject;
    public ChainPulley pulleyObject2;
    float radius = 26.0f;

    Vector3 mousePosition;
    Vector2 mouseCenter;
    bool isCrankHovered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseCenter = new Vector2(Camera.main.pixelWidth / 2.0f, Camera.main.pixelHeight / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition.x = Mouse.current.position.ReadValue().x - mouseCenter.x;
        mousePosition.y = Mouse.current.position.ReadValue().y - mouseCenter.y;

        Vector2 mouseToButtonCenter = mousePosition - transform.position;
        float distance = mouseToButtonCenter.magnitude;

        if (distance <= (GetComponent<SpriteRenderer>().bounds.size.x * radius))
        {
            mouseHoverObject.SetActive(true);
            isCrankHovered = true;
        }
        else
        {
            mouseHoverObject.SetActive(false);
            isCrankHovered = false;
        }

        if (isCrankHovered && Input.GetMouseButtonDown(0))
        {
            pulleyObject.addLink();
            pulleyObject2.removeLink();
        }

        if (isCrankHovered && Input.GetMouseButtonDown(1))
        {
            pulleyObject.removeLink();
            pulleyObject2.addLink();
        }
    }
}
