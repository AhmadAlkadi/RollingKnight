using UnityEngine;

public class FrozenBall : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    public float speed;
    public float scaleDuration = 1f; // Time to scale from 0 to 1
    public float rotationSpeed = 360f; // Degrees per second

    private float scaleTimer;


    private float timer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetAlpha(0f);

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed;

        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0,0, rot + 90);
    }

    void Update() {

        timer += Time.deltaTime;

        // Fade the FrozenBall from 0 to 1
        if (scaleTimer < scaleDuration) {
            scaleTimer += Time.deltaTime;

            // Interpolate alpha (fade-in)
            float alpha = Mathf.Lerp(0, 1, scaleTimer / scaleDuration);
            SetAlpha(alpha);
        }

        // Rotate the frozen ball infinitely
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * 2);

        //Destroy the FrozenBall after 10
        if(timer > 10) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    void SetAlpha(float alpha) {
        // Change the alpha of the sprite's color
        if (spriteRenderer != null) {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
