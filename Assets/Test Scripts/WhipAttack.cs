using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private MainCharacterAudio mainCharacterAudio;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mainCharacterAudio = GetComponent<MainCharacterAudio>();

        if (mainCharacterAudio == null)
        {
            Debug.LogWarning("MainCharacterAudio script is missing on MainCharacter!");
        }
    }

    private void Update()
    {
        // Check if the attack button is pressed
        if (Input.GetKeyDown(KeyCode.Return)) // ENTER key to attack with whip
        {
            TriggerAttack();
        }
    }

    private void TriggerAttack()
    {
        animator.SetTrigger("Attack");

        // Play the whip attack sound
        if (mainCharacterAudio != null)
        {
            mainCharacterAudio.PlayWhipAttackSound();
        }
    }
}
