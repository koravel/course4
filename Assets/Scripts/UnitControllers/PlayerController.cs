using UnityEngine;
using UnityEngine.UI;
using Controllers;

public class PlayerController : PersonController
{
    public int gunCount;
    public float shootingDelay;

    public override void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        health = maxHealth;
        textHealthBar.GetComponent<Text>().text = health.ToString();
        lastHurtTime = Time.time;
        gunsPosition = new float[] { 1.5f, -1.5f };
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
            guns[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures\\gun512x128");
            guns[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            guns[i].AddComponent<AudioSource>();
            guns[i].GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds\\weapon_player");
            guns[i].transform.position = transform.position;
            guns[i].AddComponent<BoxCollider2D>();
            guns[i].GetComponent<BoxCollider2D>().size = new Vector2(1.3f, 5.2f);
            guns[i].GetComponent<BoxCollider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterials\\PlayerBounce");
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
        movementController.Turn(Input.mousePosition);

        GetComponent<Collider2D>().isTrigger = transparency;
        foreach(var item in GetComponentsInChildren<Collider2D>())
        {
            item.isTrigger = transparency;
        }

        GetComponent<SpriteRenderer>().enabled = !invisibility;
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            item.enabled = !invisibility;
        }

        GetComponentInChildren<Canvas>().enabled = !healthInvisibility;


        healthBar.transform.parent.rotation = Quaternion.identity;

        foreach (var item in guns)
        {
            if(item != null)
            {
                item.GetComponent<ShootingController>().point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                item.GetComponent<ShootingController>().Fire(Input.GetMouseButton(0));
            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        Hurt();
    }

    public override void OnCollisionStay2D(Collision2D other)
    {
        Hurt();
    }

    public override void OnDestroy()
    {
        
    }

    public override void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementController.Move(new Vector2(horizontal, vertical));
    }

    void Hurt()
    {
        if (Time.time > (lastHurtTime + hurtDelay))
        {
            lastHurtTime = Time.time;
            if (!invulnerable)
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

    public void SetHealthBar(float myHealth, float maxHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth / maxHealth, healthBar.transform.localScale.y, 0);
        textHealthBar.GetComponent<Text>().text = myHealth.ToString();
    }
}
