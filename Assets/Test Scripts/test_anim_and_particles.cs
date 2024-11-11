using UnityEngine;

public class test_anim_and_particles : MonoBehaviour
{
    public Animator mAnim;
    public float speed = 1.0f;
    public float particleOffsetX = 0.0f;

    private Vector2 movement;
    private Rigidbody2D body;
    private ParticleSystem pSys;

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
        float x_input = Input.GetAxisRaw("Horizontal");
        float y_input = Input.GetAxisRaw("Vertical");

        Vector2 move_direction = new Vector2(Mathf.Abs(x_input) > 0.0f ? Mathf.Sign(x_input) : 0.0f, Mathf.Abs(y_input) > 0.0f ? Mathf.Sign(y_input) : 0.0f).normalized;

        mAnim.SetFloat("moveX", move_direction.x);
        mAnim.SetFloat("moveY", move_direction.y);
        mAnim.SetFloat("moveMag", Mathf.Abs(move_direction.x));

        if (Mathf.Abs(x_input) > 0.0f)
        {
            pSys.transform.localPosition = new Vector3(particleOffsetX * x_input, pSys.transform.localPosition.y, pSys.transform.localPosition.z);

            movement = new Vector2(body.position.x + Mathf.Sign(x_input) * speed, body.position.y);
            body.MovePosition(movement);
        }

    }

    public void OnFlatRoll()
    {
        pSys.Play(false);
    }
}
