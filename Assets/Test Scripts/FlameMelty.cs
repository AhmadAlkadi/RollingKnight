using UnityEngine;

public class FlameMelty : MonoBehaviour
{
    public float timeToStopMelting = 1.0f;
    [Range(1.0f, 2.0f)]
    public float growthRate = 1.01f;
    public float growthDelayTime = 0.1f;
    public float growthTimeTrigger = 0.1f;

    public float shrinkRate = 0.99f;
    public float shrinkDelayTime = 0.1f;
    public float shrinkTimeTrigger = 0.1f;

    public float knockBackForce = 1.0f;
    
    private float growthTime = 0.0f;
    private float shrinkTime = 0.0f;
    private bool isBurning = false;
    private Vector3 originalScale;
    private bool isInvoked = false;

    private PlayerElement playerElement;
    private NewPlayer playerHealth;

    public void SetIsBurning(bool is_burning)
    {
        isBurning = is_burning;

        if (!isInvoked)
        {
            Invoke(nameof(StopBurning), timeToStopMelting);
            isInvoked = true;
        }
    }

    private void StopBurning()
    {
        isBurning = false;
        isInvoked = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
        playerElement = GetComponent<PlayerElement>();
        playerHealth = GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isBurning && (playerElement.GetElementType_El() == PlayerElement.ELEMENT_TYPE.ICE))
        {
            shrinkTime += shrinkDelayTime * Time.deltaTime;
            if (shrinkTime > shrinkTimeTrigger)
            {
                shrinkTime = 0.0f;
                transform.localScale *= shrinkRate;
            }
        }
        else
        {
            growthTime += growthDelayTime * Time.deltaTime;

            if (growthTime > growthTimeTrigger)
            {
                if (transform.localScale.x < originalScale.x)
                {
                    transform.localScale = transform.localScale * growthRate;

                    if (transform.localScale.x > originalScale.x)
                    {
                        transform.localScale = originalScale;
                    }
                }

                growthTime = 0.0f;
            }
        }

        if (isBurning && (playerElement.GetElementType_El() == PlayerElement.ELEMENT_TYPE.NORMAL))
        {
            Vector3 fire_direction = transform.position;
            playerHealth.Hit(fire_direction, knockBackForce);
            isBurning = false;
        }
    }
}
