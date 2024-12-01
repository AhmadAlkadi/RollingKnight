using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStart : MonoBehaviour
{

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
        SceneManager.LoadScene("fire_test");
    }
}
