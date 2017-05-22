using System;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : PersonController
{

    public float distanceToMove;

    public override void FixedUpdate()
    {
        movementController.MoveToObject(distanceToMove);
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
    }

    void Hurt(Collision2D collision)
    {
        if (Time.time > (lastHurtTime + hurtDelay))
        {
            lastHurtTime = Time.time;
            if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
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
