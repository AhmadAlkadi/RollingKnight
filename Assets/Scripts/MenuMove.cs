using UnityEngine;

public class MenuMove : MonoBehaviour
{
    public float step = 1.0f;
    public AudioClip moveSound; 
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the GameObject!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gameObject.transform.localPosition.y + step <= -4.0f)
            {
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + step, gameObject.transform.localPosition.z);
                PlayMoveSound(); // Play sound on upward movement
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gameObject.transform.localPosition.y + step >= -5.50f)
            {
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - step, gameObject.transform.localPosition.z);
                PlayMoveSound(); // Play sound on downward movement
            }
        }
    }

    private void PlayMoveSound()
    {
        if (audioSource != null && moveSound != null)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }
}
