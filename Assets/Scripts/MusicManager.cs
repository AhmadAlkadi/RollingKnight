using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Singleton instance
    public static MusicManager Instance { get; private set; }

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip defaultMusic;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keeps MusicManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Ensure volume is set
        if (musicSource != null)
        {
            musicSource.volume = volume;

            // Play default music if available
            if (defaultMusic != null)
            {
                PlayMusic(defaultMusic);
            }
        }
        else
        {
            Debug.LogError("Music Source is not assigned in MusicManager!");
        }
    }

    /// <summary>
    /// Play the specified music clip.
    /// </summary>
    /// <param name="clip">The music clip to play.</param>
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Cannot play music: Music Source or Clip is missing!");
        }
    }

    /// <summary>
    /// Stop the current music.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Pause the current music.
    /// </summary>
    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    /// <summary>
    /// Resume the paused music.
    /// </summary>
    public void ResumeMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    /// <summary>
    /// Change the volume of the music.
    /// </summary>
    /// <param name="newVolume">The new volume (0.0 to 1.0).</param>
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0.0f, 1.0f);
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
