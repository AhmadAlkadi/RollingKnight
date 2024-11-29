using UnityEngine;

public class FirePickUp : MonoBehaviour
{
    public bool destroyOnTouch = false;
    private bool isTouchingPowerUp = false;
    private PlayerElement.ELEMENT_TYPE currentPower;
    private PlayerElement playerElement;
    private gun.GUN_TYPE gunType;
    private gun playergun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingPowerUp == true && playergun != null && playerElement != null)
        {
            playerElement.SetElement(currentPower);
            playergun.SetGun(gunType);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTouchingPowerUp = true;
            fireElement(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchingPowerUp = false;
    }

    private void fireElement(Collider2D collision)
    {
        PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
        if (currentElement != null)
        {
            gun currentGun = collision.gameObject.GetComponentInChildren<gun>();
            playerElement = currentElement;
            playergun = currentGun;
            currentElement.SetElement(PlayerElement.ELEMENT_TYPE.FIRE);
            currentGun.SetGun(gun.GUN_TYPE.FLAME);

            if (isTouchingPowerUp == true)
            {
                currentPower = PlayerElement.ELEMENT_TYPE.FIRE;
                gunType = gun.GUN_TYPE.FLAME;
            }

            if (destroyOnTouch == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
