
using System.Collections.Generic;
using UnityEngine;


public class MainMenuMovement : MonoBehaviour
{
    [SerializeField] public ParticleSystem pSysDust;
    [SerializeField] public ParticleSystem pSysFire;
    [SerializeField] public ParticleSystem pSysFreeze;
    [SerializeField] public List<bool> enablePSys;
    public float particleOffsetX = 0.0f;

    public Animator mAnim;
    public bool hasJumped = false;
    public bool isOnGround = false;

    public float castRadius = 0.25f;
    public float castDistance = 2.33f;
    public float castDistanceX = 2.33f;
    public float moveSpeed = 8.0f;
    public float moveSprintSpeed = 12.0f;
    public float moveSprintAnimSpeedFactor = 1.25f;
    public float moveFactor = 2.0f;
    public float moveDampening = 0.9f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;
    public float jumpForce => (2.0f * maxJumpHeight) / (maxJumpTime / 2.0f);
    public float gravity => (-2.0f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2.0f), 2.0f);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    private new Camera camera;
    private float inputAxis;
    private Vector2 velocity;
    private new Rigidbody2D rigidbody;

    private enum PSysType {NORMAL, FIRE, ICE};
    private bool dustState = true;
    private bool fireState = false;
    private bool freezeState = false;

    private float idleDir = -1.0f;
    private Vector2 moveDirection;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    private bool isRunning = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;

        enablePSys = new List<bool>(3);
        enablePSys.Add(true);
        enablePSys.Add(false);
        enablePSys.Add(false);

        mAnim = GetComponent<Animator>();
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
        mAnim.SetFloat("idleDir", 1.0f);

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
        hasJumped = jumping;
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            hasJumped = true;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isRunning = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
        }

        if (isRunning)
        {
            Animator anim = GetComponent<Animator>();
            mAnim.SetFloat("animMoveSpeed", moveSprintAnimSpeedFactor);

            isRunning = true;
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSprintSpeed, moveSprintSpeed * moveFactor * Time.deltaTime);
        }
        else
        {
            mAnim.SetFloat("animMoveSpeed", 1.0f);
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * moveFactor * Time.deltaTime);
        }

        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, Mathf.Abs(yInput) > 0.0f ? Mathf.Sign(yInput) : 0.0f).normalized;

        if (Mathf.Abs(inputAxisDampening) == 0.0f)
        {
            velocity.x *= moveDampening;
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

    public void EnableNormalEffectOnPlayer()
    {
        enablePSys[(int)PSysType.NORMAL] = true;
        enablePSys[(int)PSysType.FIRE] = false;
        enablePSys[(int)PSysType.ICE] = false;
    }

    public void EnableFireEffectOnPlayer()
    {
        enablePSys[(int)PSysType.NORMAL] = false;
        enablePSys[(int)PSysType.FIRE] = true;
        enablePSys[(int)PSysType.ICE] = false;
    }

    public void EnableIceEffectOnPlayer()
    {
        enablePSys[(int)PSysType.NORMAL] = false;
        enablePSys[(int)PSysType.FIRE] = false;
        enablePSys[(int)PSysType.ICE] = true;
    }
}
