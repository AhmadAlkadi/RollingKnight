using UnityEngine;

public class test_anim_and_particles : MonoBehaviour
{
    public Animator mAnim;
    public float speed = 1.0f;
    public float particleOffsetX = 0.0f;

    private float idleDir = -1.0f;
    private Vector2 movement;
    private Vector2 moveDirection;
    private Rigidbody2D body;
    private ParticleSystem pSys;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        pSys = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        mAnim.SetFloat("moveX", moveDirection.x);
        mAnim.SetFloat("moveY", moveDirection.y);
        mAnim.SetFloat("moveMag", Mathf.Abs(moveDirection.x));
        mAnim.SetFloat("idleDir", idleDir);

        if (Mathf.Abs(xInput) > 0.0f)
        {
            idleDir = Mathf.Sign(xInput);
            pSys.transform.localPosition = new Vector3(particleOffsetX * xInput, pSys.transform.localPosition.y, pSys.transform.localPosition.z);
        }

    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(Mathf.Abs(xInput) > 0.0f ? Mathf.Sign(xInput) : 0.0f, Mathf.Abs(yInput) > 0.0f ? Mathf.Sign(yInput) : 0.0f).normalized;

        movement = new Vector2(moveDirection.x * speed, body.linearVelocity.y);
        body.linearVelocity = movement;
    }

    public void OnFlatRoll()
    {
        pSys.Play(false);
    }
}
