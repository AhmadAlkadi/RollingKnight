using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite flatSprite;
    public bool noJumpDeath = false;
    public float knockBackForce = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!noJumpDeath)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
            {
                if (collision.transform.DotTest(transform, Vector2.down))
                {
                    Die();
                }
                else
                {
                    // Calculate knockback direction
                    Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

                    // Apply knockback and health reduction to the player
                    player.Hit(knockbackDirection, knockBackForce);
                }
            }
        }
        else
        {
            collision.gameObject.TryGetComponent(out Player player);
            if (player)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

                // Apply knockback and health reduction to the player
                player.Hit(knockbackDirection, knockBackForce);
            }
        }

        if (!noJumpDeath)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out NewPlayer new_player))
            {
                if (collision.transform.DotTest(transform, Vector2.down) && !noJumpDeath)
                {
                    Die();
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
        else
        {
            collision.gameObject.TryGetComponent(out NewPlayer new_player);
            if (new_player)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (new_player.transform.position - transform.position).normalized;

                // Apply knockback and health reduction to the player
                new_player.Hit(knockbackDirection, knockBackForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer  == LayerMask.NameToLayer("Bullet"))
        {
            Die();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.CompareTag("PlayerAttackBox"))
            {
                Die();
            }
        }
    }

    private void Die()
    {
        GameObject m_parent = gameObject;
        while (m_parent.transform.parent != null)
        {
            m_parent = m_parent.transform.parent.gameObject;
        }

        Collider2D collider = GetComponent<Collider2D>();
        EntityMovement entity_movement = GetComponent<EntityMovement>();
        AnimatedSprite animated_sprite = GetComponent<AnimatedSprite>();
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

        if (collider) collider.enabled = false;
        if (entity_movement) entity_movement.enabled = false;
        if (animated_sprite) animated_sprite.enabled = false;
        if (sprite_renderer) sprite_renderer.enabled = false;

        Destroy(m_parent, 0.5f);
    }
        
}
