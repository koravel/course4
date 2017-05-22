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

    public GameObject MeleeBuild(string prefabPath, Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth)
    {
        GameObject meleeEnemy = UnityEngine.Object.Instantiate(GetResourse(prefabPath));
        meleeEnemy.transform.position = position;
        meleeEnemy.transform.rotation = rotation;
        meleeEnemy.GetComponent<EnemyController>().invisibility = invisibility;
        meleeEnemy.GetComponent<EnemyController>().healthInvisibility = invisibility;
        meleeEnemy.GetComponent<EnemyController>().invulnerable = invulnerable;
        meleeEnemy.GetComponent<EnemyController>().transparency = transparency;
        meleeEnemy.GetComponent<EnemyController>().health = health;
        meleeEnemy.GetComponent<EnemyController>().maxHealth = maxHealth;
        return meleeEnemy;
    }

    public GameObject RangeBuild(string prefabPath, Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay)
    {
        GameObject rangeEnemy = MeleeBuild(prefabPath, position, rotation, transparency, invisibility, invulnerable, health, maxHealth);
        for (int i = 0; i < gunCount; i++)
        {
            rangeEnemy.GetComponent<EnemyRangeController>().guns.Add(null);
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i] = new GameObject();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].name = "ShootingController";
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].AddComponent<ShootingController>();
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject = rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().gameObject;
            rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().burstDelay = shootingDelay;
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
            if (gunCount > 1)
            {
                if (i < 2)
                {
                    rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.x + gunsPosition[i], rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.y, 0);
                }
                else if (i < 4)
                {
                    rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.x + gunsPosition[i - 2] * 2, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.y + 0.3f, 0);
                }
            }
            else if (gunCount == 1)
            {
                rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition = new Vector3(rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.x, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.y + 0.3f, rangeEnemy.GetComponent<EnemyRangeController>().guns[i].GetComponent<ShootingController>().shotSpawnObject.transform.localPosition.z);
            }
        }
        return rangeEnemy;
    }

    public GameObject PlayerBuild(string prefabPath,Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay, ref Camera cam)
    {
        GameObject player = RangeBuild(prefabPath, position, rotation, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunCount, shootingDelay);
        player.GetComponent<PlayerController>().cam = cam;
        cam.GetComponent<CameraMove>().target = player.transform;
        return player;
    }

    public override void BuildMeleeEnemy(Vector3 position,Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth)
    {
        level.LevelObjects.Add(MeleeBuild("Prefabs\\Units\\EnemyMelee", position, rotation, transparency, invisibility, invulnerable, health, maxHealth));
    }

    public override void BuildRangeEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount,float shootingDelay, float distance)
    {
        GameObject rangeEnemy = RangeBuild("Prefabs\\Units\\EnemyRange", position, rotation, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunCount, shootingDelay);
        rangeEnemy.GetComponent<EnemyRangeController>().distanceToMove = distance;
        level.LevelObjects.Add(rangeEnemy);
    }

    public override void BuildPlayer(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float[] gunsPosition, int gunCount, float shootingDelay, ref Camera cam)
    {
        level.LevelObjects.Add(PlayerBuild("Prefabs\\Units\\Player", position, rotation, transparency, invisibility, invulnerable, health, maxHealth, gunsPosition, gunCount, shootingDelay, ref cam));
    }

    public override void BuildWall(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject wall = UnityEngine.Object.Instantiate(GetResourse("Prefabs\\Units\\Wall"));
        wall.transform.position = position;
        wall.transform.rotation = rotation;
        wall.transform.localScale = scale;
        wall.name = "Wall";
        level.LevelObjects.Add(wall);
        
    }

    public override void BuildBossEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth)
    {
        throw new NotImplementedException();
    }

    public override Level BuildLevel(string levelName)
    {
        Level level = new Level();
        level.LevelName = levelName;
        level.LevelObjects = new List<GameObject>();
        return level;

    }
}
