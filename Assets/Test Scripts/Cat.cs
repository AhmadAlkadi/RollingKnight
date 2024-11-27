using UnityEngine;

public class Cat : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                // player.Hit();
                // Calculate knockback direction
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                float knockbackForce = 10f; // Adjust this as needed

                // Apply knockback and health reduction to the player
                player.Hit(knockbackDirection, knockbackForce);
            }
        }
    }


    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
        
}
