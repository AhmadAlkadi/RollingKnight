using UnityEngine;

public class BallAudio
{
    // Track current surface type
    private string currentSurface = "grass";

    // Track whether walking sound is playing
    private bool isWalkingSoundPlaying = false;

    // Reference to AudioManager
    private AudioManager audioManager;

    // Reference to Collider2D
    private Collider2D colliderGround;

    public bool hasJumped = false;
    public bool isOnGround = false;

    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start(Collider2D collider, AudioManager audio_manager)
    {
        colliderGround = collider;
        if (collider == null)
        {
            Debug.LogError("Collider2D instance not found!");
        }

        audioManager = audio_manager;
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager instance not found!");
        }
    }

    public void SetHasJumped(bool has_jumped)
    {
        hasJumped = has_jumped;
    }

    public void SetIsOnGround(bool is_on_ground)
    {
        isOnGround = is_on_ground;
    }

    public void AudioUpdate(Vector2 velocity)
    {        
        bool was_on_ground = isOnGround;
        isOnGround = colliderGround.IsTouchingLayers();

        if (!isFalling)
        {
            isFalling = (velocity.y < 0.0f) && !isOnGround;
        }

        if (isFalling && isOnGround && (audioManager != null))
        {
            // Sound on Landed
            audioManager.PlayLandingSound();
            isFalling = false;
        }

        if (hasJumped && (audioManager != null))
        {
            // Sound on Jump
            audioManager.PlayJumpSound();
            hasJumped = false;
        }
    }

    public void OnCollisionSounds(Collision2D collision)
    {
        string previousSurface = currentSurface;

        // Check for the tag and assign the surface
        if (collision.gameObject.CompareTag("Grass"))
        {
            currentSurface = "grass";
        }
        else if (collision.gameObject.CompareTag("Sand"))
        {
            currentSurface = "sand";
        }
        else if (collision.gameObject.CompareTag("Snow"))
        {
            currentSurface = "snow";
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            currentSurface = "rock";
        }
        else
        {
            Debug.LogWarning($"Surface tag not recognized: {collision.gameObject.tag}");
            currentSurface = "unknown";
        }

        // Update walking sound if surface has changed
        if (previousSurface != currentSurface && isWalkingSoundPlaying && audioManager != null)
        {
            audioManager.UpdateWalkingSound(currentSurface);
        }
    }

    public void HandleWalkingSound()
    {
        float x_input = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(x_input) > 0.0f && isOnGround)
        {
            if (!isWalkingSoundPlaying && audioManager != null)
            {
                audioManager.PlayWalkingSound(currentSurface); // Loop walking sound
                isWalkingSoundPlaying = true;
            }
            else if (isWalkingSoundPlaying && audioManager != null)
            {
                // Check if surface has changed and update the walking sound accordingly
                audioManager.UpdateWalkingSound(currentSurface);
            }
        }
        else
        {
            if (isWalkingSoundPlaying && audioManager != null)
            {
                audioManager.StopWalkingSound();
                isWalkingSoundPlaying = false;
            }
        }
    }

    public void OnJumpRollSound()
    {
        if (!isOnGround && (audioManager != null))
        {
            // Play bounce sound
            audioManager.PlayBounceSound();
        }
    }

    public void OnFlatRollSound()
    {
        if (isOnGround && !hasJumped && (audioManager != null))
        {
            // Play rolling sound based on the surface type
            audioManager.PlayRollingSound(currentSurface);
        }
    }

    public void PerformWhipAttackSound()
    {
        if (audioManager != null)
        {
            audioManager.PlayWhipAttackSound();
        }
    }
}
