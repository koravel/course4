using UnityEngine;

public class PlayerController : RangeController
{

    public Camera cam;

    public override void Start()
    {
        base.Start();
        cam.GetComponent<CameraMove>().target = gameObject.transform;
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
                item.GetComponent<ShootingController>().FireOnClick(Input.GetMouseButton(0));
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

    public override void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementController.Move(new Vector2(horizontal, vertical));
    }

    private void Hurt()
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
}
