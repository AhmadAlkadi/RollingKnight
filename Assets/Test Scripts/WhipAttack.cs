using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string attackAnimationName = "left_ball_attack";

    // Update is called once per frame
    void Update()
    {
        // Check if the attack button is pressed
        if (Input.GetButtonDown("Attack")) // "Fire1" is default for mouse or ctrl/space
        {
            // Trigger the attack animation
            TriggerAttack();
        }
    }

    // Function to trigger the attack animation
    private void TriggerAttack()
    {
        animator.SetTrigger(attackAnimationName); // Set the trigger to start the attack animation
    }
}
