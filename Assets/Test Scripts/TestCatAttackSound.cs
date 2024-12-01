using UnityEngine;

public class TestCatAttackSound : MonoBehaviour
{
    [SerializeField] private Animator catAnimator; // Reference to the Animator component
    [SerializeField] private float attackInterval = 8f; // Time between attacks

    private float attackTimer; // Timer to track time until the next attack
    private bool isAttacking = false; // Ensures attack animation only plays once per cycle

    private void Start()
    {
        // Ensure the Animator is assigned
        if (catAnimator == null)
        {
            catAnimator = GetComponent<Animator>();
            if (catAnimator == null)
            {
                Debug.LogError("Animator component is missing!");
            }
        }

        // Initialize the attack timer
        attackTimer = attackInterval;
    }

    private void Update()
    {
        // Countdown the timer if not attacking
        if (!isAttacking)
        {
            attackTimer -= Time.deltaTime;

            // Trigger the attack animation when the timer reaches 0
            if (attackTimer <= 0)
            {
                TriggerAttackAnimation();
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        if (catAnimator != null && !isAttacking)
        {
            isAttacking = true; // Lock to prevent multiple triggers
            catAnimator.Play("cat_attack"); // Trigger attack animation
            Debug.Log("Cat attack animation triggered: cat_attack");

            // Schedule a return to idle animation after the attack animation ends
            float attackAnimationLength = GetAnimationClipLength("cat_attack");
            Invoke(nameof(ReturnToIdleAnimation), attackAnimationLength);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned or already attacking!");
        }
    }

    private void ReturnToIdleAnimation()
    {
        if (catAnimator != null)
        {
            catAnimator.Play("cat_idle"); // Transition back to idle
            Debug.Log("Cat returned to idle animation: cat_idle");
            isAttacking = false; // Unlock for the next attack cycle
            attackTimer = attackInterval; // Reset the attack timer
        }
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (catAnimator != null)
        {
            foreach (AnimationClip clip in catAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip.length;
                }
            }
        }
        Debug.LogWarning($"Animation clip '{clipName}' not found!");
        return 0.5f; // Fallback duration if the clip isn't found
    }
}
