using UnityEngine;
using System.Collections.Generic;

public class test_anim_and_particles : MonoBehaviour
{
    public Animator mAnim;
    public float speed = 1.0f;
    public float particleOffsetX = 0.0f;
    public bool isOnGround = false;
    public bool hasJumped = false;
    public float jumpForce = 30.0f;

    [SerializeField] public ParticleSystem pSysDust;
    [SerializeField] public ParticleSystem pSysFire;
    [SerializeField] public ParticleSystem pSysFreeze;
    [SerializeField] public List<bool> enablePSys;

    public AudioManager audioManager;

    private float idleDir = -1.0f;
    private Vector2 moveDirection;
    private Rigidbody2D body;
    private bool dustState = true;
    private bool fireState = false;
    private bool freezeState = false;

    private Collider2D colliderGround;

    private float xInput = 0.0f;

    private BallAudio ballAudio;

    void Start()
    {
        ballAudio = new BallAudio();
        enablePSys = new List<bool>(3) { true, false, false };

        mAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        colliderGround = GetComponentInChildren<Collider2D>();
        ballAudio.Start(colliderGround, audioManager);
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
            ballAudio.hasJumped = hasJumped;
        }

        if (dustState != enablePSys[0] && pSysDust)
        {
            dustState = enablePSys[0];
        }

        if (fireState != enablePSys[1] && pSysFire)
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

        if (freezeState != enablePSys[2] && pSysFreeze)
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

            if (pSysDust)
            {
                pSysDust.transform.localPosition = new Vector3(particleOffsetX * xInput, pSysDust.transform.localPosition.y, pSysDust.transform.localPosition.z);
            }
        }

        ballAudio.HandleWalkingSound();
    }

    private void FixedUpdate()
    {
        isOnGround = colliderGround.IsTouchingLayers();
        mAnim.SetBool("isOnGround", isOnGround);
        mAnim.SetFloat("upVelocity", body.linearVelocity.y);

        ballAudio.isOnGround = isOnGround;
        ballAudio.AudioUpdate(body.linearVelocity);

        if (hasJumped && isOnGround)
        {
            // Apply jump force using AddForce with Impulse mode
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            hasJumped = false;   
        }

        // Handle horizontal movement
        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, 0.0f);

        float horizontalVelocity = moveDirection.x * speed;
        body.linearVelocity = new Vector2(horizontalVelocity, body.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballAudio != null)
        {
            ballAudio.OnCollisionSounds(collision);
        }
    }

    public void OnJumpRoll()
    {
        if ((!isOnGround && !hasJumped && pSysDust) || (!isOnGround && hasJumped) && dustState && pSysDust)
        {
            pSysDust.Play(true);
        }

        if (ballAudio != null)
        {
            ballAudio.OnJumpRollSound();
        }
    }

    public void OnFlatRoll()
    {
        if (isOnGround && !hasJumped && dustState && pSysDust)
        {
            pSysDust.Play(true);
        }

        if (ballAudio != null)
        {
            ballAudio.OnFlatRollSound();
        }
    }
}
