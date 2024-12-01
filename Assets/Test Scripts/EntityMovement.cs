
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    public float castRadius = 0.25f;
    public float castDistance = 1.57f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (rb.Raycast(direction, castRadius, castDistance, 0.0f, 0.0f)) {
            direction = -direction;
        }

        if (rb.Raycast(Vector2.down, castRadius, castDistance, 0.0f, 0.0f)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        if (direction.x > 0f) {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else if (direction.x < 0f) {
            transform.localEulerAngles = Vector3.zero;
        }
    }

}