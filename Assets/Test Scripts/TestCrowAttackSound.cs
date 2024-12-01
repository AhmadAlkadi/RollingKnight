using UnityEngine;

public class TestCrowAttackSound : MonoBehaviour
{
    [SerializeField] private Animator crowAnimator; // Reference to the Animator component
    [SerializeField] private float attackInterval = 8f; // Time between attacks

    private float attackTimer; // Timer to track time until the next attack

    private void Start()
    {
        // Ensure the Animator is assigned
        if (crowAnimator == null)
        {
            crowAnimator = GetComponent<Animator>();
            if (crowAnimator == null)
            {
                Debug.LogError("Animator component is missing!");
            }
        }

        // Initialize the attack timer
        attackTimer = attackInterval;
    }

    private void Update()
    {
        // Countdown the timer
        attackTimer -= Time.deltaTime;

        // Trigger the attack animation when the timer reaches 0
        if (attackTimer <= 0)
        {
            TriggerAttackAnimation();
            ResetAttackTimer();
        }
    }

    private void TriggerAttackAnimation()
    {
        if (crowAnimator != null)
        {
            crowAnimator.Play("crow_attacking"); // Trigger attack animation
            Debug.Log("Crow attack animation triggered: crow_attacking");

            // Schedule a return to flying animation after the attack animation ends
            float attackAnimationLength = crowAnimator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(ReturnToFlyingAnimation), attackAnimationLength);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned!");
        }
    }

    private void ResetAttackTimer()
    {
        // Reset the timer to the fixed attack interval
        attackTimer = attackInterval;
        Debug.Log($"Next crow attack in {attackInterval:F2} seconds.");
    }

    private void ReturnToFlyingAnimation()
    {
        if (crowAnimator != null)
        {
            crowAnimator.Play("crow_flying");
            Debug.Log("Crow returned to flying animation: crow_flying");
        }
    }
}
