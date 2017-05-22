using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelDecomposer
{
    public LevelModel levelModel = new LevelModel();
    
    public WallModel WallDecompose(GameObject obj)
    {
        return new WallModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale);
    }

    public MeleeModel MeleeDecompose(GameObject obj)
    {
        return new MeleeModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<MeleeController>().transparency, obj.GetComponent<MeleeController>().invisibility, obj.GetComponent<MeleeController>().invulnerable, obj.GetComponent<MeleeController>().health, obj.GetComponent<MeleeController>().maxHealth, obj.GetComponent<MeleeController>().distanceToMove, obj.GetComponent<MeleeController>().distanceOfVision, obj.GetComponent<MeleeController>().hurtDelay, obj.GetComponent<MeleeController>().scorePoints);
    }

    public RangeModel RangeDecompose(GameObject obj)
    {
        return new RangeModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<RangeController>().transparency, obj.GetComponent<RangeController>().invisibility, obj.GetComponent<RangeController>().invulnerable, obj.GetComponent<RangeController>().health, obj.GetComponent<RangeController>().maxHealth, new List<Vector2>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => new Vector2(sc.transform.localPosition.x, sc.transform.localPosition.y))), new List<float>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => sc.transform.localEulerAngles.z)), new List<float>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => sc.transform.localScale.x)), obj.GetComponent<RangeController>().gunCount, obj.GetComponent<RangeController>().shootingDelay, obj.GetComponent<RangeController>().distanceToMove, obj.GetComponent<RangeController>().distanceOfVision, obj.GetComponent<RangeController>().hurtDelay, obj.GetComponent<RangeController>().scorePoints, obj.GetComponent<RangeController>().bulletPrefab);
    }

    public PlayerModel PlayerDecompose(GameObject obj)
    {
        return new PlayerModel(obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj.GetComponent<RangeController>().transparency, obj.GetComponent<RangeController>().invisibility, obj.GetComponent<RangeController>().invulnerable, obj.GetComponent<RangeController>().health, obj.GetComponent<RangeController>().maxHealth, new List<Vector2>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => new Vector2(sc.transform.localPosition.x, sc.transform.localPosition.y))), new List<float>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => sc.transform.localEulerAngles.z)), new List<float>(obj.GetComponentsInChildren<ShootingController>().ToList().Select(sc => sc.transform.localScale.x)), obj.GetComponent<RangeController>().gunCount, obj.GetComponent<RangeController>().shootingDelay, obj.GetComponent<RangeController>().hurtDelay, obj.GetComponent<RangeController>().scorePoints, obj.GetComponent<RangeController>().bulletPrefab);
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
