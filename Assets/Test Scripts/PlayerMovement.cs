using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Camera camera;
    private float inputAxis;
    private Vector2 velocity;
    private new Rigidbody2D rigidbody;

    public float castRadius = 0.25f;
    public float castDistance = 1.52f;
    public float moveSpeed = 8.0f;
    public float moveFactor = 2.0f;
    public float moveDampening = 0.9f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;
    public float jumpForce => (2.0f * maxJumpHeight) / (maxJumpTime / 2.0f);
    public float gravity => (-2.0f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2.0f), 2.0f);
    public bool grounded {get; private set;}
    public bool jumping {get; private set;}
    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update() {

        HorizontalMovement();
        grounded = rigidbody.Raycast(Vector2.down, castRadius, castDistance);

        if(grounded) {
            GroundedMovement();
        }
        
        ApplyGravity();
    }

    private void GroundedMovement() {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        if(Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    void ApplyGravity() {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void HorizontalMovement() {
        inputAxis = Input.GetAxis("Horizontal");
        float inputAxisDampening = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * moveFactor * Time.deltaTime);

        if (Mathf.Abs(inputAxisDampening) == 0.0f)
        {
            velocity.x *= moveDampening;
        }

        if(rigidbody.Raycast(Vector2.right * velocity.x, castRadius, castDistance)) {
            velocity.x = 0f;
        }

        if(velocity.x > 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        } else if (velocity.x < 0f){
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        position.x = Mathf.Clamp(position.x,leftEdge.x + 0.8f,rightEdge.x - 0.5f);
        rigidbody.MovePosition(position);
    }
}