using UnityEngine;
using Utilites.Level;

public abstract class LevelBuilder
{
    public Level level;
    public abstract void BuildMeleeEnemy(MeleeModel meleeModel);
    public abstract void BuildRangeEnemy(RangeModel rangemodel);
    //public abstract void BuildBossEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth);
    public abstract void BuildWall(WallModel wallModel);
    public abstract void BuildPlayer(RangeModel rangeModel, ref Camera cam);
    public abstract Level BuildLevel(string levelName);
}
