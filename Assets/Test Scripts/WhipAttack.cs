using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string attackLeftAnimation = "left_ball_attack";
    public string attackRightAnimation = "right_ball_attack";

    // Update is called once per frame
    void Update()
    {
        // Check if the attack button is pressed
        if (Input.GetButtonDown("Attack")) // ENTER key to attack with whip
        {
            // Trigger the attack animation
            Debug.Log("Attack button pressed.");
            TriggerAttack();
        }
    }

    // Function to trigger the attack animation
    private void TriggerAttack()
    {
        
        //float moveX = animator.GetFloat("moveX");
        animator.SetTrigger("Attack");

        /*
        if (moveX == 1) // Facing right
        {
            animator.SetTrigger(attackRightAnimation);
        }
        else if (moveX == -1) // Facing left
        {
            animator.SetTrigger(attackLeftAnimation);
        }
        */

    }
}
