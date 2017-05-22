using UnityEngine;

public class RangeModel : MeleeModel
{
    public float[] gunsPosition;
    public int gunCount;
    public float shootingDelay;

    public RangeModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay, float distanceToMove, float distanceOfVision) : 
        base(position, rotation, transparency, invisibility, invulnerable, health, maxHealth, distanceToMove, distanceOfVision)
    {
        this.gunsPosition = gunsPosition;
        this.gunCount = gunCount;
        this.shootingDelay = shootingDelay;
    }
}
