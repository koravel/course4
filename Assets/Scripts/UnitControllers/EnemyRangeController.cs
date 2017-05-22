using System;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRangeController : PersonController
{
    public int gunCount;
    public float distanceToMove;
    public float shootingDelay;

    public override void FixedUpdate()
    {
        if(movementController.MoveToObject(distanceToMove))
        {
            movementController.MoveFromObject(distanceToMove);
        }
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        Hurt(other);
    }

    public override void OnCollisionStay2D(Collision2D other)
    {
        Hurt(other);

    }

    public override void OnDestroy()
    {

    }

    public override void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        lastHurtTime = Time.time;
        movementController.objMoveTo = GameObject.FindWithTag("Player");
        health = maxHealth;
        textHealthBar.GetComponent<Text>().text = health.ToString();
        gunsPosition = new float[] { 0.3f, -0.3f };
        for (int i = 0; i < gunCount; i++)
        {
            guns.Add(new GameObject());
            guns[i].name = "ShootingController";
            guns[i].AddComponent<ShootingController>();
            guns[i].GetComponent<ShootingController>().shotSpawn = guns[i].GetComponent<ShootingController>().transform;
            guns[i].GetComponent<ShootingController>().burstDelay = shootingDelay;
            guns[i].GetComponent<ShootingController>().shot = Resources.Load<GameObject>("Prefabs\\Units\\BulletLazer");
            guns[i].transform.SetParent(transform);
            guns[i].AddComponent<SpriteRenderer>();
            guns[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures\\gun64");
            guns[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            guns[i].AddComponent<AudioSource>();
            guns[i].GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds\\weapon_enemy");
            guns[i].transform.position = transform.position;
            guns[i].AddComponent<BoxCollider2D>();
            guns[i].GetComponent<BoxCollider2D>().size = new Vector2(0.18f, 0.65f);
            guns[i].GetComponent<BoxCollider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterials\\EnemyBounce");
            if (gunCount > 1)
            {
                if (i < 2)
                {
                    guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition = new Vector3(guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.x + gunsPosition[i], guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.y, 0);
                }
                else if (i < 4)
                {
                    guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition = new Vector3(guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.x + gunsPosition[i - 2] * 2, guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.y + 0.3f, 0);
                }
            }
            else if (gunCount == 1)
            {
                guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition = new Vector3(guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.x, guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.y + 0.3f, guns[i].GetComponent<ShootingController>().shotSpawn.transform.localPosition.z);
            }
        }
    }

    public override void Update()
    {
        healthBar.transform.parent.rotation = Quaternion.identity;

        GetComponent<Collider2D>().isTrigger = transparency;
        foreach (var item in GetComponentsInChildren<Collider2D>())
        {
            item.isTrigger = transparency;
        }

        GetComponent<SpriteRenderer>().enabled = !invisibility;
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            item.enabled = !invisibility;
        }

        GetComponentInChildren<Canvas>().enabled = !healthInvisibility;

        foreach (var item in guns)
        {
            if (item != null)
            {
                GameObject searchingObj = GameObject.FindGameObjectWithTag("Player");
                if(searchingObj != null)
                {
                    item.GetComponent<ShootingController>().point = searchingObj.transform.position;
                    item.GetComponent<ShootingController>().Fire(Vector2.Distance(transform.position, searchingObj.transform.position) <= guns[0].GetComponent<ShootingController>().shot.GetComponent<BulletController>().speed * guns[0].GetComponent<ShootingController>().shot.GetComponent<DestroyByTime>().lifeTime);
                }
            }
        }

    }

    void Hurt(Collision2D collision)
    {
        if (Time.time > (lastHurtTime + hurtDelay))
        {
            lastHurtTime = Time.time;
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
            {
                if(!invulnerable)
                {
                    health -= 20;
                }
                SetHealthBar(health, maxHealth);
                if (health <= 0)
                {
                    Instantiate(explosionAnimation, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetHealthBar(float myHealth, float maxHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth / maxHealth, healthBar.transform.localScale.y, 0);
        textHealthBar.GetComponent<Text>().text = myHealth.ToString();
    }
}
