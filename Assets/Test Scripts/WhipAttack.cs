using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the attack button is pressed
        if (Input.GetKeyDown(KeyCode.Return)) // ENTER key to attack with whip
        {
            // Trigger the attack animation
            //Debug.Log("Attack button pressed.");
            TriggerAttack();
        }
    }

    // Function to trigger the attack animation
    private void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}
