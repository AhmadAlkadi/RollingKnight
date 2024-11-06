using UnityEngine;

public class testmove : MonoBehaviour
{
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement_x = Input.GetAxis("Horizontal");

        if (Mathf.Abs(movement_x) > 0.0f)
        {
            transform.position += new Vector3(Mathf.Sign(movement_x) * 0.01f, 0.0f);
        }
    }

    void FixedUpdate()
    {
        CheckGround();

        var xInput = Input.GetAxis("Horizontal");
        var yInput = Input.GetAxis("Vertical");
        float drag = 0.1f;

        if (grounded && xInput == 0 && yInput == 0)
        {
            body.linearVelocity *= drag;
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }
}
