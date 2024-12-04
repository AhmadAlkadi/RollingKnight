
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    public float castRadius = 3f;
    public float castDistance = 1.2f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        // Trigger walking animation if moving
        animator.SetBool("isWalking", velocity.x != 0);

        // // Draw the forward ray
        // Vector2 forwardStart = rb.position;
        // Vector2 forwardEnd = forwardStart + direction.normalized * castDistance;
        // Debug.DrawLine(forwardStart, forwardEnd, Color.red);
        // // Draw the downward ray
        // Vector2 downwardStart = rb.position;
        // Vector2 downwardEnd = downwardStart + Vector2.down * castDistance;
        // Debug.DrawLine(downwardStart, downwardEnd, Color.blue);

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