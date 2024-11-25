using UnityEngine;
using UnityEngine.InputSystem;

public class Pulley : MonoBehaviour
{
    public GameObject mouseHoverObject;
    public ChainPulley pulleyObject;
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

        //print("distance: " + distance + " -- sprite.x: " + GetComponent<SpriteRenderer>().bounds.size.x * 26.0f);

        if (distance <= (GetComponent<SpriteRenderer>().bounds.size.x * 26.0f))
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
        }

        if (isCrankHovered && Input.GetMouseButtonDown(1))
        {
            pulleyObject.removeLink();
        }
    }
}
