using UnityEngine;
using System;
using Utilites.Level;
using System.Collections.Generic;

public class ArcadeLevelBuilder : LevelBuilder
{
    private GameObject GetResourse(string prefabPath)
    {
        return Resources.Load<GameObject>(prefabPath);
    }

    public GameObject MeleeBuild(string prefabPath, MeleeModel meleeModel)
    {
        GameObject meleeEnemy = UnityEngine.Object.Instantiate(GetResourse(prefabPath));
        meleeEnemy.transform.position = meleeModel.position;
        meleeEnemy.transform.rotation = meleeModel.rotation;
        meleeEnemy.GetComponent<EnemyController>().invisibility = meleeModel.invisibility;
        meleeEnemy.GetComponent<EnemyController>().healthInvisibility = meleeModel.invisibility;
        meleeEnemy.GetComponent<EnemyController>().invulnerable = meleeModel.invulnerable;
        meleeEnemy.GetComponent<EnemyController>().transparency = meleeModel.transparency;
        meleeEnemy.GetComponent<EnemyController>().health = meleeModel.health;
        meleeEnemy.GetComponent<EnemyController>().maxHealth = meleeModel.maxHealth;
        meleeEnemy.GetComponent<EnemyController>().distanceToMove = meleeModel.distanceToMove;
        meleeEnemy.GetComponent<EnemyController>().distanceOfVision = meleeModel.distanceOfVision;
        return meleeEnemy;
    }

    public GameObject RangeBuild(string prefabPath, RangeModel rangeModel)
    {
        GameObject rangeEnemy = MeleeBuild(prefabPath, rangeModel);
        for (int i = 0; i < rangeModel.gunCount; i++)
        {
            rangeEnemy.GetComponent<EnemyRangeController>().guns.Add(null);
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i] = new GameObject();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].name = "ShootingController";
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].AddComponent<ShootingController>();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().burstDelay = rangeModel.shootingDelay;
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shot = Resources.Load<GameObject>("Prefabs\\Units\\BulletLazer");
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].AddComponent<SpriteRenderer>();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures\\gun64");
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].AddComponent<AudioSource>();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds\\weapon_enemy");
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.SetParent(rangeEnemy.gameObject.transform);
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.position = rangeEnemy.gameObject.transform.position;
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].AddComponent<BoxCollider2D>();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.7f);
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<BoxCollider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterials\\EnemyBounce");
            if (rangeModel.gunCount > 1)
            {
                if (i < 2)
                {
                    rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.x + rangeModel.gunsPosition[i], rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.y, 0);
                }
                else if (i < 4)
                {
                    rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.x + rangeModel.gunsPosition[i - 2] * 2, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.y + 0.3f, 0);
                }
            }
            else if (rangeModel.gunCount == 1)
            {
                rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.x, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.y + 0.3f, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].transform.localPosition.z);
            }
        }
        return rangeEnemy;
    }

    public GameObject PlayerBuild(string prefabPath, RangeModel rangeModel, ref Camera cam)
    {
        GameObject player = RangeBuild(prefabPath, rangeModel);
        player.GetComponent<PlayerController>().cam = cam;
        cam.GetComponent<CameraMove>().target = player.transform;
        return player;
    }

    public GameObject WallBuild(string prefabPath, WallModel wallModel)
    {
        GameObject wall = UnityEngine.Object.Instantiate(GetResourse(prefabPath));
        wall.transform.position = wallModel.position;
        wall.transform.rotation = wallModel.rotation;
        wall.transform.localScale = wallModel.scale;
        wall.name = "Wall";
        return wall;
    }

    public override void BuildMeleeEnemy(MeleeModel meleeModel)
    {
        level.LevelObjects.Add(MeleeBuild("Prefabs\\Units\\EnemyMelee", meleeModel));
    }

    public override void BuildRangeEnemy(RangeModel rangeModdel)
    {
        level.LevelObjects.Add(RangeBuild("Prefabs\\Units\\EnemyRange", rangeModdel));
    }

    public override void BuildPlayer(RangeModel rangeModdel, ref Camera cam)
    {
        level.LevelObjects.Add(PlayerBuild("Prefabs\\Units\\Player", rangeModdel, ref cam));
    }

    public override void BuildWall(WallModel wallModel)
    {
        level.LevelObjects.Add(WallBuild("Prefabs\\Units\\Wall", wallModel));
    }

    public override void BuildBossEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth)
    {
        throw new NotImplementedException();
    }

    public override Level BuildLevel(string levelName, string backGroundSprite)
    {
        Level level = new Level();
        level.LevelName = levelName;
        level.LevelObjects = new List<GameObject>();
        return level;
    }
}
