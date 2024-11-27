using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerElement : MonoBehaviour
{
    public enum ELEMENT_TYPE { NORMAL, FIRE, ICE };
    public ELEMENT_TYPE currentElementType = ELEMENT_TYPE.NORMAL;

    private List<GameObject> pBullets = new List<GameObject>();
    private List<TurretBullet> bullets = new List<TurretBullet>();

    public float attackCoolDownFlame = 0.35f;
    public float fireRate = 10.0f;
    private float cooldownTimer = Mathf.Infinity;
    private GameObject shootLocation;
    private Vector3 gunDirection;

    public void SetDirection(Vector3 direction)
    {
        gunDirection = direction;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentElementType)
        {
            case ELEMENT_TYPE.NORMAL:
                break;
            case ELEMENT_TYPE.FIRE:

                if (Input.GetKey(KeyCode.K) && (cooldownTimer > attackCoolDownFlame))
                {
                    //ShootFlame(10.0f);
                }
                break;
            case ELEMENT_TYPE.ICE:
                break;
        }
    }

    public int GetIntELEMENT_TYPE()
    {
        int element = 0;
        switch (currentElementType)
        {
            case ELEMENT_TYPE.NORMAL:
                break;
            case ELEMENT_TYPE.FIRE:
                element = 1;
                break;
            case ELEMENT_TYPE.ICE:
                element = 2;
                break;
        }
        return element;
    }

    public void SetElement(ELEMENT_TYPE element_type)
    {
        currentElementType = element_type;
    }
}
