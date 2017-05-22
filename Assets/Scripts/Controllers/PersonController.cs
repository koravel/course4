using System.Collections.Generic;
using UnityEngine;

public abstract class PersonController : MonoBehaviour
{
    public MovementController movementController;
    public float health;
    public float maxHealth;
    public float lastHurtTime;
    public float hurtDelay;
    public bool invulnerable;
    public bool invisibility;
    public bool healthInvisibility;
    public bool transparency;
    public GameObject explosionAnimation;
    public GameObject healthBar;
    public GameObject textHealthBar;
    public float distanceToMove;
    public float distanceOfVision;

    public abstract void Start();

    public abstract void FixedUpdate();

    public abstract void Update();

    public abstract void OnCollisionEnter2D(Collision2D other);

    public abstract void OnDestroy();

    public abstract void OnCollisionStay2D(Collision2D other);
}
