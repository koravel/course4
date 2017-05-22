using UnityEngine;
using Utilites.Level;

public abstract class LevelBuilder
{
    public Level level;
    public abstract void BuildMeleeEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth);
    public abstract void BuildRangeEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay, float distance, Transform parentTransform);
    public abstract void BuildBossEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth);
    public abstract void BuildWall(Vector3 position, Quaternion rotation, Vector3 scale);
    public abstract void BuildPlayer(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay, Transform parentTransform,Camera cam);
    public abstract Level BuildLevel(string levelName);
}
