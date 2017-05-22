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
        try
        {
            meleeEnemy.name = "EnemyMelee";
            meleeEnemy.transform.position = meleeModel.position;
            meleeEnemy.transform.rotation = meleeModel.rotation;
            meleeEnemy.GetComponent<MeleeController>().invisibility = meleeModel.invisibility;
            meleeEnemy.GetComponent<MeleeController>().healthInvisibility = meleeModel.invisibility;
            meleeEnemy.GetComponent<MeleeController>().invulnerable = meleeModel.invulnerable;
            meleeEnemy.GetComponent<MeleeController>().transparency = meleeModel.transparency;
            meleeEnemy.GetComponent<MeleeController>().health = meleeModel.health;
            meleeEnemy.GetComponent<MeleeController>().maxHealth = meleeModel.maxHealth;
            meleeEnemy.GetComponent<MeleeController>().distanceToMove = meleeModel.distanceToMove;
            meleeEnemy.GetComponent<MeleeController>().distanceOfVision = meleeModel.distanceOfVision;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return meleeEnemy;
    }

    public GameObject RangeBuild(string prefabPath, RangeModel rangeModel)
    {
        GameObject rangeEnemy = MeleeBuild(prefabPath, rangeModel);
        try
        {
            rangeEnemy.name = "EnemyRange";
            rangeEnemy.GetComponent<RangeController>().gunsPosition = rangeModel.gunsPosition;
            rangeEnemy.GetComponent<RangeController>().gunsRotation = rangeModel.gunsRotation;
            rangeEnemy.GetComponent<RangeController>().gunCount = rangeModel.gunCount;
            for (int i = 0; i < rangeModel.gunCount; i++)
            {
                rangeEnemy.GetComponent<RangeController>().guns.Add(null);
                rangeEnemy.GetComponent<RangeController>().guns[i] = new GameObject();
                rangeEnemy.GetComponent<RangeController>().guns[i].name = "ShootingController";
                rangeEnemy.GetComponent<RangeController>().guns[i].AddComponent<ShootingController>();
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<ShootingController>().burstDelay = rangeModel.shootingDelay;
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<ShootingController>().shot = Resources.Load<GameObject>(GlobalData.prefabBulletLazer);
                rangeEnemy.GetComponent<RangeController>().guns[i].AddComponent<SpriteRenderer>();
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GlobalData.textureGun);
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
                rangeEnemy.GetComponent<RangeController>().guns[i].AddComponent<AudioSource>();
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(GlobalData.soundGun);
                rangeEnemy.GetComponent<RangeController>().guns[i].transform.SetParent(rangeEnemy.gameObject.transform);
                rangeEnemy.GetComponent<RangeController>().guns[i].transform.position = rangeEnemy.gameObject.transform.position;
                rangeEnemy.GetComponent<RangeController>().guns[i].AddComponent<BoxCollider2D>();
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.7f);
                rangeEnemy.GetComponent<RangeController>().guns[i].GetComponent<BoxCollider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>(GlobalData.materialGun);
                rangeEnemy.GetComponent<RangeController>().guns[i].transform.localPosition = rangeEnemy.GetComponent<RangeController>().gunsPosition[i];
                rangeEnemy.GetComponent<RangeController>().guns[i].transform.localRotation = rangeEnemy.GetComponent<RangeController>().gunsRotation[i];
                rangeEnemy.GetComponent<RangeController>().guns[i].transform.localScale = new Vector3(1, 1, 0);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return rangeEnemy;
    }

    public GameObject PlayerBuild(string prefabPath, RangeModel rangeModel, ref Camera cam)
    {
        GameObject player = RangeBuild(prefabPath, rangeModel);
        try
        {
            player.name = "Player";
            player.GetComponent<PlayerController>().cam = cam;
            cam.GetComponent<CameraMove>().target = player.transform;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return player;
    }

    public GameObject WallBuild(string prefabPath, WallModel wallModel)
    {
        GameObject wall = UnityEngine.Object.Instantiate(GetResourse(prefabPath));
        try
        {
            wall.transform.position = wallModel.position;
            wall.transform.rotation = wallModel.rotation;
            wall.transform.localScale = wallModel.scale;
            wall.name = "Wall";
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return wall;
    }

    public override void BuildMeleeEnemy(MeleeModel meleeModel)
    {
        level.LevelObjects.Add(MeleeBuild(GlobalData.prefabMelee, meleeModel));
    }

    public override void BuildRangeEnemy(RangeModel rangeModdel)
    {
        level.LevelObjects.Add(RangeBuild(GlobalData.prefabRange, rangeModdel));
    }

    public override void BuildPlayer(RangeModel rangeModdel, ref Camera cam)
    {
        level.LevelObjects.Add(PlayerBuild(GlobalData.prefabPlayer, rangeModdel, ref cam));
    }

    public override void BuildWall(WallModel wallModel)
    {
        level.LevelObjects.Add(WallBuild(GlobalData.prefabWall, wallModel));
    }

    //public override void BuildBossEnemy(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth)
    //{
    //    throw new NotImplementedException();
    //}

    public override Level BuildLevel(string levelName)
    {
        Level level = new Level();
        level.LevelName = levelName;
        level.LevelObjects = new List<GameObject>();
        return level;
    }
}
