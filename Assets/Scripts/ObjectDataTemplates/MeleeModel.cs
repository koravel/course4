using UnityEngine;

public class MeleeModel : ObjectModel
{
    public bool transparency;
    public bool invisibility;
    public bool invulnerable;
    public float health;
    public float maxHealth;
    public float distanceToMove;
    public float distanceOfVision;

    public MeleeModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float distanceToMove, float distanceOfVision) : base(position, rotation)
    {
        this.transparency = transparency;
        this.invisibility = invisibility;
        this.invulnerable = invulnerable;
        this.health = health;
        this.maxHealth = maxHealth;
        this.distanceToMove = distanceToMove;
        this.distanceOfVision = distanceOfVision;
    }
}
