using UnityEngine;
using System.Collections.Generic;

public class test_anim_and_particles : MonoBehaviour
{
    public Animator mAnim;
    public float speed = 1.0f;
    public float particleOffsetX = 0.0f;
    public bool isOnGround = false;
    public bool hasJumped = false;

    [SerializeField] public ParticleSystem pSysDust;
    [SerializeField] public ParticleSystem pSysFire;
    [SerializeField] public ParticleSystem pSysFreeze;
    [SerializeField] public List<bool> enablePSys;

    private float idleDir = -1.0f;
    private Vector2 moveDirection;
    private Rigidbody2D body;
    private bool dustState = true;
    private bool fireState = false;
    private bool freezeState = false;

    private Collider2D colliderGround;

    private float xInput = 0.0f;

    // Reference to AudioManager
    private AudioManager audioManager;

    // Track current surface type
    private string currentSurface = "grass";

    // Track whether walking sound is playing
    private bool isWalkingSoundPlaying = false;

    void Start()
    {
        enablePSys = new List<bool>(3) { true, false, false };

        mAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        colliderGround = GetComponentInChildren<Collider2D>();

        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found!");
        }
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
        }

        if (dustState != enablePSys[0])
        {
            dustState = enablePSys[0];
        }

        if (fireState != enablePSys[1])
        {
            fireState = enablePSys[1];

            if (fireState)
            {
                pSysFire.Play(true);
            }
            else
            {
                pSysFire.Stop();
            }
        }

        if (freezeState != enablePSys[2])
        {
            freezeState = enablePSys[2];

            if (freezeState)
            {
                pSysFreeze.Play(true);
            }
            else
            {
                pSysFreeze.Stop();
            }
        }

        mAnim.SetFloat("moveX", moveDirection.x);
        mAnim.SetFloat("moveY", 0.0f);
        mAnim.SetFloat("moveMag", Mathf.Abs(moveDirection.x));
        mAnim.SetFloat("idleDir", idleDir);

        if (Mathf.Abs(xInput) > 0.0f)
        {
            idleDir = Mathf.Sign(xInput);
            pSysDust.transform.localPosition = new Vector3(
                particleOffsetX * xInput,
                pSysDust.transform.localPosition.y,
                pSysDust.transform.localPosition.z
            );
        }

        HandleWalkingSound();
    }

    private void FixedUpdate()
    {
        bool wasOnGround = isOnGround;
        isOnGround = colliderGround.IsTouchingLayers();
        mAnim.SetBool("isOnGround", isOnGround);
        mAnim.SetFloat("upVelocity", body.linearVelocity.y);

        if (!wasOnGround && isOnGround)
        {
            // Landed
            if (audioManager != null)
            {
                audioManager.PlayLandingSound();
            }
        }

        if (hasJumped && isOnGround)
        {
            // Jump
            if (audioManager != null)
            {
                audioManager.PlayJumpSound();
            }

            // Apply jump force using AddForce with Impulse mode
            float jump_value = 30.0f; 
            body.AddForce(new Vector2(0, jump_value), ForceMode2D.Impulse);

            hasJumped = false;
        }

        // Handle horizontal movement
        moveDirection = new Vector2(
            Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f,
            0.0f
        );

        float horizontalVelocity = moveDirection.x * speed;
        body.linearVelocity = new Vector2(horizontalVelocity, body.linearVelocity.y);
    }

    private void HandleWalkingSound()
    {
        if (Mathf.Abs(xInput) > 0.0f && isOnGround)
        {
            if (!isWalkingSoundPlaying && audioManager != null)
            {
                audioManager.PlayWalkingSound(currentSurface); // Loop walking sound
                isWalkingSoundPlaying = true;
            }
            else if (isWalkingSoundPlaying && audioManager != null)
            {
                // Check if surface has changed and update the walking sound accordingly
                audioManager.UpdateWalkingSound(currentSurface);
            }
        }
        else
        {
            if (isWalkingSoundPlaying && audioManager != null)
            {
                audioManager.StopWalkingSound();
                isWalkingSoundPlaying = false;
            }
        }
    }

    public void OnJumpRoll()
    {
        if (!isOnGround && dustState)
        {
            //pSysDust.Play(true);

            // Play bounce sound
            if (audioManager != null)
            {
                audioManager.PlayBounceSound();
            }
        }
    }

    public void OnFlatRoll()
    {
        if (isOnGround && !hasJumped && dustState)
        {
            pSysDust.Play(true);

            // Play rolling sound based on the surface type
            if (audioManager != null)
            {
                audioManager.PlayRollingSound(currentSurface);
            }
        }
    }

    public void PerformWhipAttack()
    {
        if (audioManager != null)
        {
            audioManager.PlayWhipAttackSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string previousSurface = currentSurface;

        // Check for the tag and assign the surface
        if (collision.gameObject.CompareTag("Grass"))
        {
            currentSurface = "grass";
        }
        else if (collision.gameObject.CompareTag("Sand"))
        {
            currentSurface = "sand";
        }
        else if (collision.gameObject.CompareTag("Snow"))
        {
            currentSurface = "snow";
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            currentSurface = "rock";
        }
        else
        {
            Debug.LogWarning($"Surface tag not recognized: {collision.gameObject.tag}");
            currentSurface = "unknown"; 
        }

        // Update walking sound if surface has changed
        if (previousSurface != currentSurface && isWalkingSoundPlaying && audioManager != null)
        {
            audioManager.UpdateWalkingSound(currentSurface);
        }
    }
}
