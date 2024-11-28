using UnityEngine;
using System.Collections;

public class flammable : MonoBehaviour
{
    public float timeToVanish = 5.0f;

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
        if(collision.gameObject.layer == 3)
        {
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
            TurretBullet TurrentElemnt = collision.gameObject.GetComponent<TurretBullet>();
            if(TurrentElemnt != null)
            {
                child.SetActive(true);
                Invoke(nameof(setInActive), timeToVanish);
            }
            if(currentElement != null)
            {
                if (currentElement.GetIntELEMENT_TYPE() == 1)
                {
                    child.SetActive(true);
                    Invoke(nameof(setInActive), timeToVanish);
                }
            }
        }

        if (collision.gameObject.layer == 8)
        {
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
            TurretBullet TurrentElemnt = collision.gameObject.GetComponent<TurretBullet>();
            if (TurrentElemnt != null)
            {
                child.SetActive(true);
                Invoke(nameof(setInActive), timeToVanish);
            }
            if (currentElement != null)
            {
                if (currentElement.GetIntELEMENT_TYPE() == 1)
                {
                    child.SetActive(true);
                    Invoke(nameof(setInActive), timeToVanish);
                }
            }
        }
    }

    public void setInActive()
    {
        gameObject.SetActive(false);
    }
}
