using UnityEngine;

public class Freezing : MonoBehaviour
{
    public float timeToVanish = 5.0f;
    Rigidbody2D m_Rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
            if(collision.gameObject.name == "HitBox")
            {
                currentElement = collision.gameObject.transform.parent.GetComponent<PlayerElement>();
            }
            TurretBullet TurrentElemnt = collision.gameObject.GetComponent<TurretBullet>();
            var colliderMain = gameObject.GetComponentInParent<Collider2D>();
            colliderMain.isTrigger = false;
            if (TurrentElemnt != null)
            {
                child.SetActive(true);
            }
            if (currentElement != null)
            {
                if (currentElement.GetIntELEMENT_TYPE() == 1)
                {
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    Invoke(nameof(setInActive), timeToVanish);
                }
                else if (currentElement.GetIntELEMENT_TYPE() == 2)
                {
                    m_Rigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    if(gameObject.transform.GetChild(3) != null)
                        gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    child.SetActive(true);
                }
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            PlayerElement currentElement = collision.gameObject.GetComponent<PlayerElement>();
            TurretBullet TurrentElemnt = collision.gameObject.GetComponent<TurretBullet>();

            if (TurrentElemnt != null)
            {
                child.SetActive(true);
            }
            if (currentElement != null)
            {
                if (currentElement.GetIntELEMENT_TYPE() == 1) // Fire
                {
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    Invoke(nameof(setInActive), timeToVanish);
                }
                else if (currentElement.GetIntELEMENT_TYPE() == 2) // Ice
                {
                    m_Rigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    if (gameObject.transform.GetChild(3) != null)
                        gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    child.SetActive(true);
                }
            }
        }
    }

    public void setInActive()
    {
        gameObject.SetActive(false);
    }
}
