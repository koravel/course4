using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : RangeModel
{
    public PlayerModel() : base()
    {

    }

    public PlayerModel(Vector3 position, Quaternion rotation, Vector3 localScale, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, List<Vector2> gunsPosition, List<float> gunsRotation, List<float> gunsScale, int gunCount, float shootingDelay, float hurtDelay, float scorePoints, string bulletPrefab) : 
        base(position, rotation, localScale, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunsRotation, gunsScale, gunCount, shootingDelay, 0, 0, hurtDelay, 0, bulletPrefab)
    {

    }
}
