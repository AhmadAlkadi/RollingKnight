using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Camera camera;
    private float inputAxis;
    private Vector2 velocity;
    private new Rigidbody2D rigidbody;

    
    public float moveSpeed = 8.0f;
    public float moveFactor = 2.0f;
    public float moveDampening = 0.9f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;
    public float jumpForce => (2.0f * maxJumpHeight) / (maxJumpTime / 2.0f);
    public float gravity => (-2.0f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2.0f), 2.0f);
    public bool grounded {get; private set;}
    public bool jumping {get; private set;}
    //Boolean variable to track if the player is facing right
    public bool isFacingRight { get; private set; } = true;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update() {

        HorizontalMovement();
        grounded = rigidbody.Raycast(Vector2.down);

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

        if(rigidbody.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        if(velocity.x > 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            isFacingRight = true;
        } else if (velocity.x < 0f){
            transform.eulerAngles = Vector3.zero;
            isFacingRight = false;
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

    private void OnCollisionEnter2D(Collision2D collision ) {
        if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) {
            if(transform.DotTest(collision.transform, Vector2.up)) {
                velocity.y = 0f;
            }
        }
    }
}
