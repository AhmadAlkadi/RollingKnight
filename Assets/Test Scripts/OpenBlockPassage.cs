using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenBlockPassage : MonoBehaviour
{
    public GameObject blockPassage;

    private Rigidbody2D blockPassageRB;
    private BoxCollider2D blockPassageCollider;
    private HingeJoint2D blockPassageHinge;

    bool isHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
    }

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
        if (isHit)
        {
            blockPassageCollider.enabled = true;
            blockPassageHinge.enabled = true;
            blockPassageRB.bodyType = RigidbodyType2D.Dynamic;

            isHit = false;
        }
    }
}
