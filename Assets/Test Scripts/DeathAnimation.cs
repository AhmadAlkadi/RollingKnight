using UnityEngine;
using System.Collections;

public class DeathAnimation: MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] deathSprites;
    public float frameDuration = 0.2f;

    private void Reset() {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable(){
        UpdateSprite();
        DisablePhysics();
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

            
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (deathSprites.Length > 0) {
            float fadeDuration = 1f; // Duration of the fade-out
            float fadeElapsed = 0f;
            Color spriteColor = spriteRenderer.color; // Preserve original color

            while (fadeElapsed < fadeDuration) {
                fadeElapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration); // Gradually reduce alpha
                spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
                yield return null; // Wait for the next frame
            }

            spriteRenderer.enabled = false; // Hide the sprite completely after fade-out
    }


    }
}
