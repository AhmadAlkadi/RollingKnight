using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic; // Assign your main menu music clip in the Inspector
    private AudioSource audioSource;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    private void Awake()
    {
        // Ensure this script is not destroyed when switching between scenes
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.clip = mainMenuMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        // Start playing the main menu music
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0.0f, 1.0f);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    private void OnDestroy()
    {
        // Ensure music stops when leaving the main menu scene
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
