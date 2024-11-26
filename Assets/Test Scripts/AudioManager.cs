using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;          // For one-shot SFX
    [SerializeField] private AudioSource loopingSFXSource;   // For looping SFX

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Character Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip whipAttackSound;

    [Header("Walking Sounds by Surface")]
    [SerializeField] private AudioClip grassWalkingSound;
    [SerializeField] private AudioClip sandWalkingSound;
    [SerializeField] private AudioClip snowWalkingSound;
    [SerializeField] private AudioClip rockWalkingSound;

    [Header("Rolling Sounds by Surface")]
    [SerializeField] private AudioClip grassRollingSound;
    [SerializeField] private AudioClip sandRollingSound;
    [SerializeField] private AudioClip snowRollingSound;
    [SerializeField] private AudioClip rockRollingSound;

    private Dictionary<string, AudioClip> walkingSounds;
    private Dictionary<string, AudioClip> rollingSounds;

    private Coroutine fadeOutCoroutine;
    private string currentWalkingSurface = "";

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keeps AudioManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize dictionaries for walking and rolling sounds
        walkingSounds = new Dictionary<string, AudioClip>
        {
            { "grass", grassWalkingSound },
            { "sand", sandWalkingSound },
            { "snow", snowWalkingSound },
            { "rock", rockWalkingSound }
        };

        rollingSounds = new Dictionary<string, AudioClip>
        {
            { "grass", grassRollingSound },
            { "sand", sandRollingSound },
            { "snow", snowRollingSound },
            { "rock", rockRollingSound }
        };

        // Start background music if available
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    /// <summary>
    /// Play a single sound effect.
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Play looping walking sound based on the surface type.
    /// </summary>
    /// <param name="surfaceType">The surface type (e.g., "grass").</param>
    public void PlayWalkingSound(string surfaceType)
    {
        if (walkingSounds.TryGetValue(surfaceType, out AudioClip clip))
        {
            if (loopingSFXSource.clip != clip)
            {
                loopingSFXSource.clip = clip;
                loopingSFXSource.volume = 1.0f; // Ensure volume is set
                loopingSFXSource.loop = true;
                loopingSFXSource.Play();
                currentWalkingSurface = surfaceType;
            }
            else if (!loopingSFXSource.isPlaying)
            {
                loopingSFXSource.volume = 1.0f;
                loopingSFXSource.Play();
                currentWalkingSurface = surfaceType;
            }
        }
    }

    /// <summary>
    /// Update the walking sound if the surface has changed.
    /// </summary>
    /// <param name="surfaceType">The new surface type.</param>
    public void UpdateWalkingSound(string surfaceType)
    {
        if (currentWalkingSurface != surfaceType)
        {
            PlayWalkingSound(surfaceType);
        }
    }

    /// <summary>
    /// Stop the looping walking sound with a fade-out.
    /// </summary>
    public void StopWalkingSound()
    {
        if (loopingSFXSource.isPlaying)
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutine = StartCoroutine(FadeOutLoopingSFX(loopingSFXSource, 0.2f)); // Fade out over 0.2 seconds
        }
    }

    private IEnumerator FadeOutLoopingSFX(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        float startTime = Time.time;
        while (audioSource.volume > 0)
        {
            float elapsed = Time.time - startTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, elapsed / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume
        audioSource.clip = null;
        audioSource.loop = false;
        currentWalkingSurface = "";
        fadeOutCoroutine = null;
    }

    /// <summary>
    /// Play a rolling sound based on the surface type.
    /// </summary>
    /// <param name="surfaceType">The surface type (e.g., "grass").</param>
    public void PlayRollingSound(string surfaceType)
    {
        if (rollingSounds.TryGetValue(surfaceType, out AudioClip clip))
        {
            PlaySFX(clip);
        }
    }

    /// <summary>
    /// Play character jump sound.
    /// </summary>
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

    /// <summary>
    /// Play character bounce sound.
    /// </summary>
    public void PlayBounceSound()
    {
        PlaySFX(bounceSound);
    }

    /// <summary>
    /// Play character landing sound.
    /// </summary>
    public void PlayLandingSound()
    {
        PlaySFX(landingSound);
    }

    /// <summary>
    /// Play character whip attack sound.
    /// </summary>
    public void PlayWhipAttackSound()
    {
        PlaySFX(whipAttackSound);
    }

    /// <summary>
    /// Play background music.
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
    }

    /// <summary>
    /// Stop background music.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
