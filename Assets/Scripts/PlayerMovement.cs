using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float inputAxis;
    private Vector2 velocity;
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 8f;
    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update() {
        HorizontalMovement();
    }

    private void HorizontalMovement() {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x,inputAxis * moveSpeed,moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }
}
