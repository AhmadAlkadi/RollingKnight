/***************************************************************
*file: Turret_Targeting.cs
*author: Sean Butler
*author: Ahmad Alkadi
*class: CS 4700 – Game Development
*assignment: program 3
*date last modified: 10/6/2024
*
*purpose: The gun type of what the player will be holding
*
*References:
*https://docs.unity3d.com/ScriptReference/index.html
*
****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static PlayerElement;

public class gun : MonoBehaviour
{
    public enum GUN_TYPE {NORMAL, ICE, FLAME};
    public GUN_TYPE currentGunType = GUN_TYPE.FLAME;
    public GUN_TYPE forceGunType = GUN_TYPE.FLAME;

    public float attackCoolDownNormal = 0.5f;
    public float attackCoolDownMachine = 0.07f;
    public float attackCoolDownFlame = 0.35f;
    public float attackCoolDownLaser = 0.01f;
    public float fireRate = 10.0f;
    public float rapidFireRate = 1.05f;
    public int laserLength = 10;
    public float laserSpacing = 0.15f;
    [Range(0.0f, 2.0f)]
    public float spreadShotDirection = 1.0f;
    public bool allowRapidFire = false;
    public bool forceWeapon = false;
    private float idleDir = -1.0f;

    public GameObject[] bulletObjects { get; private set; }
    public int numberOfBullets = 20;
    public GameObject playerDirection;

    [SerializeField] private GameObject bulletNormal, bulletMachine, bulletSpread, bulletFlame;
    [SerializeField] private GameObject turret_bullet;
    private List<GameObject> pBullets = new List<GameObject>();
    private List<TurretBullet> bullets = new List<TurretBullet>();
    private List<(int, float)> laserIndexList = new List<(int, float)>();

    private GameObject shootLocation;
    private float cooldownTimer = Mathf.Infinity;
    private float attackCoolDown = 1.0f;
    private Vector3 gunDirection;

    private GameObject parent;

    public void SetGun(GUN_TYPE gun_type)
    {
        currentGunType = gun_type;
    }

    public void SetRapidFire(bool allow)
    {
        allowRapidFire = allow;
    }

    public void SetDirection(Vector3 direction)
    {
        gunDirection = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject;
        while (parent.transform.parent != null)
        {
            parent = transform.parent.gameObject;
        }

        bulletObjects = new GameObject[] { bulletNormal, bulletMachine, bulletSpread, bulletFlame };
        const int target_location_child_index = 1; // relies on the target_location gameobject being the 4th child at the moment
        shootLocation = this.transform.GetChild(target_location_child_index).gameObject;

        for (int i=0; i<numberOfBullets; i++)
        {
            var newBullet = Instantiate(turret_bullet);
            newBullet.gameObject.layer = LayerMask.NameToLayer("Bullet");
            pBullets.Add(newBullet);

            var bulletChild = newBullet.transform.GetChild(0).gameObject.GetComponent<TurretBullet>();
            bulletChild.gameObject.layer = LayerMask.NameToLayer("Bullet");
            bulletChild.gameObject.GetComponent<CircleCollider2D>().excludeLayers |= (1 << LayerMask.NameToLayer("Player")) ;
            bullets.Add(bulletChild);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");

        gunDirection = new Vector3(xInput, yInput, 0.0f);

        Debug.Log(gunDirection);

        if (Mathf.Abs(xInput) > 0.0f)
        {
            idleDir = Mathf.Sign(xInput);
        }
        Vector3 normGunDirection = gunDirection.normalized;

        if (gunDirection == Vector3.zero)
        {
            normGunDirection.x =  idleDir;
        }

        Vector3 shoot_direction = transform.position - shootLocation.transform.position;
        shoot_direction.Normalize();

        if (Input.GetKey(KeyCode.K) && (cooldownTimer > attackCoolDown))
        {
            float localFireRate = fireRate;

            if (allowRapidFire)
            {
                localFireRate *= rapidFireRate;
            }

            if (forceWeapon)
            {
                currentGunType = forceGunType;
            }

            switch (currentGunType)
            {
                case GUN_TYPE.NORMAL:
                    break;

                case GUN_TYPE.ICE:
                    attackCoolDown = attackCoolDownMachine;
                    ShootNormal(normGunDirection, localFireRate);
                    break;
                case GUN_TYPE.FLAME:
                    attackCoolDown = attackCoolDownFlame;
                    ShootFlame(normGunDirection, localFireRate);
                    break;
            }

            cooldownTimer = 0.0f;
        }

        cooldownTimer += Time.deltaTime;
    }

    private void ShootNormal(Vector3 bullet_direction, float bullet_speed)
    {
        bool shoot_target = true;
        if (shoot_target)
        {
            int index = FindInActiveBullet();

            if (index >= 0)
            {
                bullets[index].SetPosition(shootLocation.transform.position.x, shootLocation.transform.position.y);
                bullets[index].SetOffsetPoint(new Vector3(0.0f, 0.0f));
                bullets[index].SetSpeed(bullet_speed);
                bullets[index].SetRotateSpeed(0.0f);
                bullets[index].SetRadius(0.0f);
                bullet_direction.Normalize();

                if (bullet_direction.x == 0)
                {
                    bullets[index].SetDirection(0.0f, Mathf.Sign(bullet_direction.y));
                }
                else if (bullet_direction.y == 0)
                {
                    bullets[index].SetDirection(Mathf.Sign(bullet_direction.x), 0.0f);
                }
                else
                {
                    bullets[index].SetDirection(Mathf.Sign(bullet_direction.x), Mathf.Sign(bullet_direction.y));
                }
            }
        }
    }

    private void ShootFlame(Vector3 bullet_direction, float bullet_speed)
    {
        bool shoot_target = true;
        if (shoot_target)
        {
            int index = FindInActiveBullet();

            if (index >= 0)
            {
                float local_radius = 1.0f;
                float local_offset_x = 1.5f;
                float local_offset_y = 1.5f;

                float x_direction = (shootLocation.transform.position - parent.transform.position).x;
                float y_direction = (shootLocation.transform.position - parent.transform.position).y;
                if (x_direction < 0.0f)
                {
                    local_offset_x = -local_offset_x;
                }

                if (y_direction < 0.0f)
                {
                    local_offset_y = -local_offset_y;
                }

                bullets[index].SetPosition(parent.transform.position.x, parent.transform.position.y);
                bullets[index].SetOffsetPoint(new Vector3(local_offset_x, 0.0f));
                bullets[index].SetSpeed(bullet_speed);
                bullets[index].SetRotateSpeed(10.0f);
                bullets[index].SetRadius(local_radius);
                bullet_direction.Normalize();

                if (bullet_direction.x == 0)
                {
                    bullets[index].SetOffsetPoint(new Vector3(0.0f, local_offset_y));
                    bullets[index].SetDirection(0.0f, Mathf.Sign(bullet_direction.y));
                }
                else if (bullet_direction.y == 0)
                {
                    bullets[index].SetDirection(Mathf.Sign(bullet_direction.x), 0.0f);
                }
                else
                {
                    bullets[index].SetOffsetPoint(new Vector3(local_offset_x, local_offset_y));
                    bullets[index].SetDirection(Mathf.Sign(bullet_direction.x), Mathf.Sign(bullet_direction.y));
                }
            }
        }
    }


    private int FindInActiveBullet()
    {
        int bullet_index = -1;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullet_index = i;
                break;
            }
        }

        return bullet_index;
    }

    public int GetIntELEMENT_TYPE()
    {
        int element = 0;
        switch (currentGunType)
        {
            case GUN_TYPE.NORMAL:
                break;
            case GUN_TYPE.FLAME:
                element = 1;
                break;
            case GUN_TYPE.ICE:
                element = 2;
                break;
        }
        return element;
    }

    private int FindInActiveBullet(in List<(int, float)> list)
    {
        int bullet_index = -1;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy && !list.Exists(x => x.Item1 == i))
            {
                bullet_index = i;
                break;
            }
        }

        return bullet_index;
    }
}
