using UnityEngine;

public class Cat : MonoBehaviour
{
    public Sprite flatSprite;
    public float knockBackForce = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } 
            else
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

                // Apply knockback and health reduction to the player
                player.Hit(knockbackDirection, knockBackForce);
            }
        }

        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out NewPlayer new_player))
        {
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (new_player.transform.position - transform.position).normalized;

                // Apply knockback and health reduction to the player
                new_player.Hit(knockbackDirection, knockBackForce);
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
