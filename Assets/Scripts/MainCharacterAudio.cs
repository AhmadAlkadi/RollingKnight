using UnityEngine;

public class MainCharacterAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] landSounds;
    [SerializeField] private AudioClip[] runningStartSounds; // One-shot sound for starting sprint
    [SerializeField] private AudioClip[] walkingSounds; // Sounds for walking (looping)
    [SerializeField] private AudioClip[] whipAttackSounds;

    private AudioSource audioSource;
    private NewPlayerMovement playerMovement;
    private bool isWalking = false;
    private bool sprintSoundPlayed = false; // Ensures sprint sound plays only once when Shift is pressed
    private bool wasGrounded = false; // To track previous grounded state for landing sound

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on MainCharacter!");
        }

        playerMovement = GetComponent<NewPlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("NewPlayerMovement script is missing on MainCharacter!");
        }
    }

    private void Update()
    {
        HandleWalkingSound();
        HandleSprintSound();
        HandleLandingSound(); // Check for landing
    }

    private void HandleWalkingSound()
    {
        // Check if there is horizontal movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0 && playerMovement.isOnGround)
        {
            if (!isWalking)
            {
                isWalking = true;
                PlayRandomSound(walkingSounds, true); // Start looping walking sound
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                StopWalkingSound(); // Stop walking sound when idle
            }
        }
    }

    private void HandleSprintSound()
    {
        // Play the sprint start sound when Shift is pressed
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && playerMovement.isOnGround && !sprintSoundPlayed)
        {
            PlayRandomSound(runningStartSounds, false);
            sprintSoundPlayed = true;
        }

        // Reset sprintSoundPlayed when Shift key is released
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            sprintSoundPlayed = false;
        }
    }

    private void HandleLandingSound()
    {
        // Detect if the player transitions from not grounded to grounded
        if (!wasGrounded && playerMovement.isOnGround)
        {
            PlayRandomSound(landSounds, false); // Play land sound
        }

        // Update the grounded state for the next frame
        wasGrounded = playerMovement.isOnGround;
    }

    /// <summary>
    /// Plays a random jump sound. To be called from the animator or other scripts.
    /// </summary>
    public void PlayJumpSound()
    {
        PlayRandomSound(jumpSounds, false);
    }

    /// <summary>
    /// Plays a random whip attack sound.
    /// </summary>
    public void PlayWhipAttackSound()
    {
        PlayRandomSound(whipAttackSounds, false);
    }

    /// <summary>
    /// Helper method to play a random sound from an array.
    /// </summary>
    private void PlayRandomSound(AudioClip[] clips, bool loop)
    {
        if (audioSource == null || clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.loop = loop;
        audioSource.clip = clip;

        if (loop)
        {
            audioSource.Play(); // For looping sounds
        }
        else
        {
            audioSource.PlayOneShot(clip); // For one-shot sounds
        }
    }

    /// <summary>
    /// Stops the walking sound when the player is idle.
    /// </summary>
    private void StopWalkingSound()
    {
        if (audioSource && audioSource.isPlaying && audioSource.loop)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}
