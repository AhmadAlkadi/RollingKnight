using UnityEngine;
using System.Collections;

public class DeathAnimation: MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] deathSprites;
    public float frameDuration = 0.2f;

    private void Reset() {
        // SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // if (spriteRenderers == null) {
        //     Debug.Log("There is no sprire renderer detected");
        // }
        // foreach (SpriteRenderer sr in spriteRenderers) {
        //     if (sr.enabled) {
        //         spriteRenderer = sr; // Assign the first active SpriteRenderer
        //         Debug.Log("Found Something");
        //         break;
        //     }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
        

    private void OnEnable(){
        UpdateSprite();
        DisablePhysics();
        DisableAnimator();
        StartCoroutine(Animate());
    }

    private void UpdateSprite(){
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        // if(deadSprite != null) {
        //     spriteRenderer.sprite = deadSprite;
        // }
    }

    private void DisablePhysics() {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D collider in colliders) {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //GetComponent<PlayerMovement>().enabled = false;
        //GetComponent<EntityMovement>().enabled = false;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if(playerMovement != null) {
            playerMovement.enabled = false;
        }

        if(entityMovement != null) {
            entityMovement.enabled = false;
        }
    }

    private void DisableAnimator() {
        Animator animator = GetComponentInChildren<Animator>();

        if(animator != null) {
            animator.enabled = false;
        }
    }


    private IEnumerator Animate() {
        float elapsed = 0f;
        float duration = 3f;

    
        int currentFrame = 0;

        while(elapsed < duration) {
            if (deathSprites.Length > 0 && currentFrame < deathSprites.Length) {
                spriteRenderer.sprite = deathSprites[currentFrame];
                currentFrame = Mathf.Min(currentFrame + 1, deathSprites.Length - 1);
                yield return new WaitForSeconds(frameDuration); // Wait for the frame duration
            }

            // Fade out the last sprite
            if (currentFrame == deathSprites.Length - 1) {
                spriteRenderer.sprite = deathSprites[deathSprites.Length - 1]; // Ensure the last frame is set

                float fadeDuration = 1f; // Time it takes to fade out
                float fadeElapsed = 0f;
                Color spriteColor = spriteRenderer.color; // Preserve the original color

                while (fadeElapsed < fadeDuration) {
                    fadeElapsed += Time.deltaTime;
                    float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration); // Gradually reduce alpha
                    spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
                    yield return null; // Wait for the next frame
                }

                spriteRenderer.enabled = false; // Hide the sprite after fading out
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        


    }
}
