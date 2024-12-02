using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenBlockPassage : MonoBehaviour
{
    public GameObject blockPassage;
    public bool forceTrigger;

    private Rigidbody2D blockPassageRB;
    private BoxCollider2D blockPassageCollider;
    private HingeJoint2D blockPassageHinge;

    bool isHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerElement player_element = null;

            if (collision.gameObject.CompareTag("PlayerAttackBox"))
            {
                collision.gameObject.transform.parent.TryGetComponent<PlayerElement>(out player_element);
            }
            else
            {
                collision.gameObject.TryGetComponent<PlayerElement>(out player_element);
            }

            if (player_element && player_element.GetElementType_El() == PlayerElement.ELEMENT_TYPE.FIRE)
            {
                isHit = true;
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            // need @proxadator updates code to know if the bullet is a fire type bullet
            TurretBullet currentElement = collision.gameObject.GetComponent<TurretBullet>();

            if (currentElement != null)
            {
                if (currentElement.isFire())
                {
                    isHit = true;
                }
            }
        }
    }

    private void objectDeactivate() { gameObject.SetActive(false); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockPassageRB = blockPassage.GetComponent<Rigidbody2D>();
        blockPassageCollider = blockPassage.GetComponent<BoxCollider2D>();
        blockPassageHinge = blockPassage.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (forceTrigger)
        {
            isHit = forceTrigger;
        }

        if (isHit)
        {
            blockPassageCollider.enabled = true;
            blockPassageHinge.enabled = true;
            blockPassageRB.bodyType = RigidbodyType2D.Dynamic;

            isHit = false;
        }
    }
}
