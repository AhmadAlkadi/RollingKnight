using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;

public class Player: MonoBehaviour
{
    public PlayerSpriteRenderer normalRenderer;
    private HealthManager healthManager;

    private DeathAnimation deathAnimation;

    private PlayerMovement playerMovement;
    private bool isVulnerable = true;

    private Coroutine knockbackRoutine;
    private Coroutine blinkingCoroutine; // Reference to the blinking coroutine


    public bool normal => normalRenderer.enabled;

    private void Awake(){
        deathAnimation = GetComponent<DeathAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        if(GameManager.Instance != null) {
            healthManager = GameManager.Instance.GetComponent<HealthManager>();
        }

        // Handle missing HealthManager component
        if (healthManager == null) {
            Debug.LogError("HealthManager component not found on GameManager!");
        }
    }
    // public void Hit(){
    //     if(isVulnerable) {
    //         healthManager.health--;
    //         if(healthManager.health <= 0) {
    //             Death();
    //         }else {
    //             StartCoroutine(GetHurt());
    //         }
    //     }
        
    // }

    public void Hit(Vector2 knockbackDirection, float knockbackForce) {
        if (isVulnerable) {
            float knockbackDuration = 0.8f; // Adjust duration as needed
            Knockback(knockbackDirection, knockbackForce, knockbackDuration);
            blinkingCoroutine = StartCoroutine(BlinkEffect(3f, 0.1f)); // Blink for 3 seconds with a 0.1-second interval

            healthManager.health--;

            if (healthManager.health <= 0) {
                Death();
            } else {
                StartCoroutine(GetHurt());
            }
        }
    }


    IEnumerator GetHurt(){
        //Layer 3: Player
        //Layer 8: Enemy
        //Enable ignore collision
        isVulnerable = false;
        Physics2D.IgnoreLayerCollision(3,8,true);
        yield return new WaitForSeconds(3);
        //Disable ignore collision
        Physics2D.IgnoreLayerCollision(3,8,false);
        isVulnerable = true;
    }

    private void Death() {
        if (knockbackRoutine != null) {
            StopCoroutine(knockbackRoutine);
            knockbackRoutine = null;
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = null;
        }

        normalRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
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
        PlayerMovement movement = GetComponent<PlayerMovement>();

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
