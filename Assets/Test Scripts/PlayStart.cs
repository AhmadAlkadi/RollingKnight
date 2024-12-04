using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStart : MonoBehaviour
{
    public MusicManager musicManager;
    public MainMenuMusic mainMenuMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.CompareTag("PlayerAttackBox"))
            {
                Invoke(nameof(Die), 0.4f);
            }
        }
    }

    private void Die()
    {
        musicManager.gameObject.SetActive(true);
        mainMenuMusic.gameObject.SetActive(false);
        SceneManager.LoadScene("Story");
    }
}
