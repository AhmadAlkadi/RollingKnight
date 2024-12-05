using UnityEngine;

public class CrowAudio : MonoBehaviour
{
    [Header("Wing Flapping Sounds")]
    [SerializeField] private AudioClip[] crowFlappingClips; // Array of flapping sounds

    [Header("Attacking Sounds")]
    [SerializeField] private AudioClip[] crowAttackingClips; // Array of attacking sounds

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from Crow!");
        }
    }

    // Called by Animation Event for flapping (idle/flying)
    public void PlayFlappingSound()
    {
        if (audioSource != null && crowFlappingClips.Length > 0)
        {
            AudioClip randomFlapClip = crowFlappingClips[Random.Range(0, crowFlappingClips.Length)];
            audioSource.PlayOneShot(randomFlapClip);
        }
        else
        {
            Debug.LogWarning("Flapping sounds or AudioSource is missing!");
        }
    }

    // Called by Animation Event for attacking
    public void PlayAttackingSound()
    {
        if (audioSource != null && crowAttackingClips.Length > 0)
        {
            AudioClip randomAttackClip = crowAttackingClips[Random.Range(0, crowAttackingClips.Length)];
            audioSource.PlayOneShot(randomAttackClip);
        }
        else
        {
            Debug.LogWarning("Attacking sounds or AudioSource is missing!");
        }
    }
}
