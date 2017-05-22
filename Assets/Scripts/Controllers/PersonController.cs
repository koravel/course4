using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public abstract class PersonController : ObjectController
    {
        public MovementController movementController;
        public List<GameObject> guns;
        public float[] gunsPosition;
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

        protected Rigidbody2D rigidBody;

        public abstract void Start();

        public abstract void FixedUpdate();

        public abstract void Update();

        public abstract void OnCollisionEnter2D(Collision2D other);

        public abstract void OnDestroy();

        public abstract void OnCollisionStay2D(Collision2D other);
    }
}
