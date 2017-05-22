using UnityEngine;

public class PlayerModel : RangeModel
{
    public PlayerModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay) : 
        base(position, rotation, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunCount, shootingDelay, 0, 0)
    {

    }
}
