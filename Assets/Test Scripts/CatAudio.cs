using UnityEngine;

public class CatAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] catAttackClips;
    [SerializeField] private AudioClip[] catIdleClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on CatAudio!");
        }
    }

    public void PlayAttackSound()
    {
        if (catAttackClips.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, catAttackClips.Length);
            audioSource.PlayOneShot(catAttackClips[randomIndex]);
            Debug.Log("Cat attack sound played.");
        }
    }

    public void PlayIdleSound()
    {
        if (catIdleClips.Length > 0 && audioSource != null)
        {
            // Play the idle sound only if no other sound is currently playing
            if (!audioSource.isPlaying)
            {
                int randomIndex = Random.Range(0, catIdleClips.Length);
                audioSource.clip = catIdleClips[randomIndex];
                audioSource.Play();
                Debug.Log("Cat idle sound played.");
            }
        }
    }
}
