using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : RangeModel
{
    public PlayerModel() : base()
    {

    }

    public PlayerModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, List<Vector2> gunsPosition, List<Quaternion> gunsRotation, int gunCount, float shootingDelay) : 
        base(position, rotation, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunsRotation, gunCount, shootingDelay, 0, 0)
    {

    }
}
