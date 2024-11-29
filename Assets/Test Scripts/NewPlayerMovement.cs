
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;
using UnityEngine.EventSystems;
//using UnityEngine.Windows;

public class NewPlayerMovement : MonoBehaviour
{

    private new Camera camera;
    private float inputAxis;
    private Vector2 velocity;
    private new Rigidbody2D rigidbody;

    [SerializeField] public ParticleSystem pSysDust;
    [SerializeField] public ParticleSystem pSysFire;
    [SerializeField] public ParticleSystem pSysFreeze;
    [SerializeField] public List<bool> enablePSys;

    public Animator mAnim;
    private Rigidbody2D body;
    private Collider2D colliderGround;
    public bool hasJumped = false;

    public bool isOnGround = false;

    private float idleDir = -1.0f;
    private bool dustState = true;
    private bool fireState = false;
    private bool freezeState = false;

    public float particleOffsetX = 0.0f;

    private Vector2 moveDirection;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    public float castRadius = 0.25f;
    public float castDistance = 2.33f;
    public float moveSpeed = 8.0f;
    public float moveFactor = 2.0f;
    public float moveDampening = 0.9f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;
    public float jumpForce => (2.0f * maxJumpHeight) / (maxJumpTime / 2.0f);
    public float gravity => (-2.0f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2.0f), 2.0f);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    //Boolean variable to track if the player is facing right
    public bool isFacingRight { get; private set; } = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;

        enablePSys = new List<bool>(3);
        enablePSys.Add(true);
        enablePSys.Add(false);
        enablePSys.Add(false);

        mAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        colliderGround = GetComponentInChildren<Collider2D>();
    }

    private void Update()
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

        HorizontalMovement();
        grounded = rigidbody.Raycast(Vector2.down, castRadius, castDistance);
        isOnGround = grounded;
        mAnim.SetBool("isOnGround", isOnGround);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();

    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            hasJumped = false;
        }
    }

    void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
        mAnim.SetFloat("upVelocity", velocity.y);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        float inputAxisDampening = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * moveFactor * Time.deltaTime);

        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, Mathf.Abs(yInput) > 0.0f ? Mathf.Sign(yInput) : 0.0f).normalized;

        if (Mathf.Abs(inputAxisDampening) == 0.0f)
        {
            velocity.x *= moveDampening;
        }

        if (rigidbody.Raycast(Vector2.right * velocity.x, castRadius, castDistance))
        {
            velocity.x = 0f;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.8f, rightEdge.x - 0.5f);
        rigidbody.MovePosition(position);
    }

    public void OnJumpRoll()
    {
        if ((!isOnGround && !hasJumped) || (!isOnGround && hasJumped) && dustState)
        {
            //pSysDust.Play(true);
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


//using UnityEngine;

//using System.Collections.Generic;

//public class test_anim_and_particles : MonoBehaviour
//{
//    public Animator mAnim;
//    public float speed = 1.0f;
//    public float particleOffsetX = 0.0f;
//    public bool isOnGround = false;
//    public bool hasJumped = false;
//    [SerializeField] public ParticleSystem pSysDust;
//    [SerializeField] public ParticleSystem pSysFire;
//    [SerializeField] public ParticleSystem pSysFreeze;
//    [SerializeField] public List<bool> enablePSys;

//    private float idleDir = -1.0f;
//    private Vector2 movement;
//    private Vector2 moveDirection;
//    private Rigidbody2D body;
//    private bool dustState = true;
//    private bool fireState = false;
//    private bool freezeState = false;

//    private Collider2D colliderGround;

//    private float xInput = 0.0f;
//    private float yInput = 0.0f;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        enablePSys = new List<bool>(3);
//        enablePSys.Add(true);
//        enablePSys.Add(false);
//        enablePSys.Add(false);

//        mAnim = GetComponent<Animator>();
//        body = GetComponent<Rigidbody2D>();
//        colliderGround = GetComponentInChildren<Collider2D>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (dustState != enablePSys[0])
//        {
//            dustState = enablePSys[0];
//        }

//        if (fireState != enablePSys[1])
//        {
//            fireState = enablePSys[1];

//            if (fireState)
//            {
//                pSysFire.Play(true);
//            }
//            else
//            {
//                pSysFire.Stop();
//            }
//        }

//        if (freezeState != enablePSys[2])
//        {
//            freezeState = enablePSys[2];

//            if (freezeState)
//            {
//                pSysFreeze.Play(true);
//            }
//            else
//            {
//                pSysFreeze.Stop();
//            }
//        }

//        mAnim.SetFloat("moveX", moveDirection.x);
//        mAnim.SetFloat("moveY", moveDirection.y);
//        mAnim.SetFloat("moveMag", Mathf.Abs(moveDirection.x));
//        mAnim.SetFloat("idleDir", idleDir);


//        if (Mathf.Abs(xInput) > 0.0f)
//        {
//            idleDir = Mathf.Sign(xInput);
//            pSysDust.transform.localPosition = new Vector3(particleOffsetX * xInput, pSysDust.transform.localPosition.y, pSysDust.transform.localPosition.z);
//        }

//    }

//    private void FixedUpdate()
//    {
//        xInput = Input.GetAxisRaw("Horizontal");
//        yInput = Input.GetAxisRaw("Vertical");
//        hasJumped = Input.GetKey(KeyCode.Space);

//        isOnGround = colliderGround.IsTouchingLayers();
//        mAnim.SetBool("isOnGround", isOnGround);
//        mAnim.SetFloat("upVelocity", body.linearVelocity.y);

//        float jump_value = 0.0f;

//        if (isOnGround && hasJumped)
//        {
//            jump_value = 20.0f;
//            hasJumped = false;
//        }

//        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, Mathf.Abs(yInput) > 0.0f ? Mathf.Sign(yInput) : 0.0f).normalized;

//        movement = new Vector2(moveDirection.x * speed, body.linearVelocity.y + jump_value);
//        body.linearVelocity = movement;
//    }

//    public void OnJumpRoll()
//    {
//        if ((!isOnGround && !hasJumped) || (!isOnGround && hasJumped) && dustState)
//        {
//            //pSysDust.Play(true);
//        }
//    }

//    public void OnFlatRoll()
//    {
//        if (isOnGround && !hasJumped && dustState)
//        {
//            pSysDust.Play(true);
//        }
//    }
//}
