using UnityEngine;

public class LevelDecomposer
{
    public LevelModel levelModel = new LevelModel();
    
    public WallModel WallDecompose(GameObject obj)
    {
        return new WallModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale);
    }

    public MeleeModel MeleeDecompose(GameObject obj)
    {
        return new MeleeModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<MeleeController>().transparency, obj.GetComponent<MeleeController>().invisibility, obj.GetComponent<MeleeController>().invulnerable, obj.GetComponent<MeleeController>().health, obj.GetComponent<MeleeController>().maxHealth, obj.GetComponent<MeleeController>().distanceToMove, obj.GetComponent<MeleeController>().distanceOfVision);
    }

    public RangeModel RangeDecompose(GameObject obj)
    {
        return new RangeModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<RangeController>().transparency, obj.GetComponent<RangeController>().invisibility, obj.GetComponent<RangeController>().invulnerable, obj.GetComponent<RangeController>().health, obj.GetComponent<RangeController>().maxHealth, obj.GetComponent<RangeController>().gunsPosition, obj.GetComponent<RangeController>().gunsRotation, obj.GetComponent<RangeController>().gunCount, obj.GetComponent<RangeController>().shootingDelay, obj.GetComponent<RangeController>().distanceToMove, obj.GetComponent<RangeController>().distanceOfVision);
    }

    public PlayerModel PlayerDecompose(GameObject obj)
    {
        return new PlayerModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<RangeController>().transparency, obj.GetComponent<RangeController>().invisibility, obj.GetComponent<RangeController>().invulnerable, obj.GetComponent<RangeController>().health, obj.GetComponent<RangeController>().maxHealth, obj.GetComponent<RangeController>().gunsPosition, obj.GetComponent<RangeController>().gunsRotation, obj.GetComponent<RangeController>().gunCount, obj.GetComponent<RangeController>().shootingDelay);
    }

    public gameObjectType CheckType(GameObject obj)
    {
        switch(obj.tag)
        {
            case "Wall":
                {
                    return gameObjectType.Wall;
                }
            case "Enemy":
                {
                    return gameObjectType.Melee;
                }
            case "EnemyRange":
                {
                    return gameObjectType.Range;
                }
            case "Boss":
                {
                    return gameObjectType.Boss;
                }
            case "Player":
                {
                    return gameObjectType.Player;
                }
            default:
                {
                    return gameObjectType.NULL;
                }
        }
    }

    public enum gameObjectType
    {
        Wall,
        Melee,
        Range,
        Boss,
        Player,
        NULL
    };
}
