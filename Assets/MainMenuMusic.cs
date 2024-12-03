using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic; // Assign your main menu music clip in the Inspector
    private AudioSource audioSource;

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

    private void OnDestroy()
    {
        // Ensure music stops when leaving the main menu scene
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
