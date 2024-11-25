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
    private Vector2 movement;
    private Vector2 moveDirection;
    private Rigidbody2D body;
    private bool dustState = true;
    private bool fireState = false;
    private bool freezeState = false;
    
    private Collider2D colliderGround;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enablePSys = new List<bool>(3);
        enablePSys.Add(true);
        enablePSys.Add(false);
        enablePSys.Add(false);

        mAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        colliderGround = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        mAnim.SetFloat("moveY", moveDirection.y);
        mAnim.SetFloat("moveMag", Mathf.Abs(moveDirection.x));
        mAnim.SetFloat("idleDir", idleDir);
        

        if (Mathf.Abs(xInput) > 0.0f)
        {
            idleDir = Mathf.Sign(xInput);
            pSysDust.transform.localPosition = new Vector3(particleOffsetX * xInput, pSysDust.transform.localPosition.y, pSysDust.transform.localPosition.z);
        }

    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        hasJumped = Input.GetKey(KeyCode.Space);

        isOnGround = colliderGround.IsTouchingLayers();
        mAnim.SetBool("isOnGround", isOnGround);

        float jump_value = 0.0f;

        if (isOnGround && hasJumped)
        {
            jump_value = 20.0f;
            hasJumped = false;
        }

        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, Mathf.Abs(yInput) > 0.0f ? Mathf.Sign(yInput) : 0.0f).normalized;

        movement = new Vector2(moveDirection.x * speed, body.linearVelocity.y + jump_value);
        body.linearVelocity = movement;
    }

    public void OnJumpRoll()
    {
        if ((!isOnGround && !hasJumped) || (!isOnGround && hasJumped) && dustState)
        {
            pSysDust.Play(true);
        }
    }

    public void OnFlatRoll()
    {
        if (isOnGround && !hasJumped && dustState)
        {
            pSysDust.Play(true);
        }
    }
}
