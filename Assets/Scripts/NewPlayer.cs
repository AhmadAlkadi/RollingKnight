using UnityEngine;
using System.Collections;

public class NewPlayer: MonoBehaviour
{
    public float invulnerableTimeSeconds = 3.0f;
    public float knockBackDuration = 0.8f;
    public float blinkDurationSeconds = 3.0f;
    public float blinkIntervalSeconds = 0.1f;
    public float resetLevelDelaySeconds = 3.0f;

    private HealthManager healthManager;

    private DeathAnimation deathAnimation;

    private bool isVulnerable = true;

    private Coroutine knockbackRoutine;
    private Coroutine blinkingCoroutine; // Reference to the blinking coroutine

    private void Start()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        if (GameManager.Instance != null)
        {
            healthManager = GameManager.Instance.GetComponent<HealthManager>();
        }

        // Handle missing HealthManager component
        if (healthManager == null)
        {
            Debug.LogError("HealthManager component not found on GameManager!");
        }
    }

    public void Hit(Vector2 knockbackDirection, float knockbackForce) {
        if (isVulnerable) {
            Knockback(knockbackDirection, knockbackForce, knockBackDuration);
            blinkingCoroutine = StartCoroutine(BlinkEffect(blinkDurationSeconds, blinkIntervalSeconds));

            healthManager.health--;

            if (healthManager.health <= 0) {
                Death();
            } else {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt() {
        var player_mask = LayerMask.NameToLayer("Player");
        var enemy_mask = LayerMask.NameToLayer("Enemy");

        //Enable ignore collision
        isVulnerable = false;
        Physics2D.IgnoreLayerCollision(player_mask, enemy_mask, true);
        yield return new WaitForSeconds(invulnerableTimeSeconds);
        //Disable ignore collision
        Physics2D.IgnoreLayerCollision(player_mask, enemy_mask, false);
        isVulnerable = true;
    }

    private void Death() {
        if (knockbackRoutine != null) {
            StopCoroutine(knockbackRoutine);
            knockbackRoutine = null;
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = null;
        }

        this.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(resetLevelDelaySeconds);
    }

    public void Knockback(Vector2 direction, float force, float duration) {
        // Stop any existing knockback coroutine before starting a new one
        if (knockbackRoutine != null) {
            StopCoroutine(knockbackRoutine);
        }

        knockbackRoutine = StartCoroutine(KnockbackCoroutine(direction, force, duration));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        NewPlayerMovement movement = GetComponent<NewPlayerMovement>();

        if (rb == null) yield break;

        // Disable PlayerMovement during knockback
        if (movement != null) {
            movement.enabled = false;
        }

        float elapsed = 0f;
        Vector2 knockbackVelocity = direction.normalized * force;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            // Gradually reduce velocity
            float slowdownFactor = 1 - (elapsed / duration);
            rb.linearVelocity = knockbackVelocity * slowdownFactor;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero; // Stop the player after knockback

        // Re-enable PlayerMovement after knockback
        if (movement != null) {
            movement.enabled = true;
        }
    }

    private IEnumerator BlinkEffect(float duration, float interval) {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += interval;

            // Toggle visibility
            foreach (SpriteRenderer sr in spriteRenderers) {
                sr.enabled = !sr.enabled;
            }

            yield return new WaitForSeconds(interval);
        }

        // Ensure all sprites are visible at the end
        foreach (SpriteRenderer sr in spriteRenderers) {
            sr.enabled = true;
        }
    }
}
