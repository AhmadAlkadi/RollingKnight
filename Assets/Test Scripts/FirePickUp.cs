using UnityEngine;

public class FirePickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
            gun currentGun = collision.gameObject.GetComponentInChildren<gun>();
            currentElement.SetElement(PlayerElement.ELEMENT_TYPE.FIRE);
            currentGun.SetGun(gun.GUN_TYPE.FLAME);

            gameObject.SetActive(false);
        }
    }
}
