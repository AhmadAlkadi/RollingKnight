using UnityEngine;

public class PickUpAudio : MonoBehaviour
{
    [Header("Pickup Sounds")]
    [SerializeField] private AudioClip firePickupSound;
    [SerializeField] private AudioClip icePickupSound;

    private AudioSource audioSource;

    private bool hasFirePickup = false; // Track if the player picked up a fire element
    private bool hasIcePickup = false;  // Track if the player picked up an ice element

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on PickUpAudio!");
        }
    }

    private void Update()
    {
        // Check if the player presses the K key
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayPickupSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for FirePickUp
        if (collision.CompareTag("FirePickUp"))
        {
            hasFirePickup = true;  // Set Fire pickup flag
            hasIcePickup = false; // Reset Ice pickup flag
        }
        // Check for IcePickUp
        else if (collision.CompareTag("IcePickUp"))
        {
            hasIcePickup = true;  // Set Ice pickup flag
            hasFirePickup = false; // Reset Fire pickup flag
        }
    }

    /// <summary>
    /// Plays the pickup sound based on the current pickup state when pressing K.
    /// </summary>
    private void PlayPickupSound()
    {
        if (hasFirePickup && firePickupSound != null)
        {
            audioSource.PlayOneShot(firePickupSound);
        }
        else if (hasIcePickup && icePickupSound != null)
        {
            audioSource.PlayOneShot(icePickupSound);
        }
        else
        {
            Debug.LogWarning("No pickup sound is set or no valid pickup detected!");
        }
    }
}
