using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilites.Serialiazer;
using System.Collections.Generic;
using System.Linq;

public class BuilderDirector : MonoBehaviour
{
    private ArcadeLevelBuilder builder = new ArcadeLevelBuilder();
    private LevelDecomposer decomposer = new LevelDecomposer();
    public Camera cam;
    public Image background;
    public int time = 0;
    public int score = 0;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject player;

    private void Start()
    {
        //builder.level = builder.BuildLevel(GlobalData.levelName);
        //builder.BuildPlayer(new PlayerModel(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), new Vector3(10, 10, 0), false, false, true, 2000, 2000, new List<Vector2>() { new Vector2(0.3f, 0), new Vector2(-0.3f, 0) }, new List<float>() {0,0 }, 2, 0.1f), ref cam);
        //builder.BuildMeleeEnemy(new MeleeModel(new Vector3(80, 80, 0), new Quaternion(0, 0, 0, 0), new Vector3(10, 10, 0), false, false, false, 1000, 1500, 1, 30));
        //builder.BuildRangeEnemy(new RangeModel(new Vector3(70, 70, 0), new Quaternion(0, 0, 0, 0), new Vector3(10, 10, 0), false, false, false, 1500, 1500, new List<Vector2>() { new Vector2(0.3f, 0f), new Vector2(-0.3f, 0) }, new List<float>() { 0, 0 }, 2, 0.5f, 40, 50));
        //builder.BuildWall(new WallModel(new Vector3(25, 25, 0), new Quaternion(0, 0, 0, 0), new Vector3(1, 15, 0)));
        //decomposer.levelModel.backgroundSpritePath = "Textures\\Background";
        //background.sprite = Resources.Load<Sprite>(decomposer.levelModel.backgroundSpritePath);
        //builder.BuildPrefab("Prefabs\\Units\\Boss");
      
        builder.level = builder.BuildLevel(GlobalData.levelName);
        if (File.Exists(builder.level.LevelName))
        {
            Serialiazer.DeserialiazitionFromXml(ref decomposer.levelModel, builder.level.LevelName);
            foreach (WallModel item in decomposer.levelModel.wallList)
            {
                builder.BuildWall(item);
            }
            foreach (MeleeModel item in decomposer.levelModel.meleeList)
            {
                builder.BuildMeleeEnemy(item);
            }
            foreach (RangeModel item in decomposer.levelModel.rangeList)
            {
                builder.BuildRangeEnemy(item);
            }
            builder.BuildPlayer(decomposer.levelModel.playerObj, ref cam);
            background.sprite = Resources.Load<Sprite>(decomposer.levelModel.backgroundSpritePath);
            time = decomposer.levelModel.levelTime;
            score = decomposer.levelModel.levelScore;

            player = builder.level.LevelObjects.Find(o => o.tag == "Player");
            enemies.AddRange(builder.level.LevelObjects.Where(o => o.tag.Contains("Enemy")));

            decomposer.levelModel.meleeList.Clear();
            decomposer.levelModel.rangeList.Clear();
            decomposer.levelModel.wallList.Clear();
            decomposer.levelModel.playerObj = null;
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    //public void clll()
    //{
    //    builder.level.LevelObjects.ForEach(o => { Destroy(o); });
    //    builder.level.LevelObjects.Clear();
    //}

    public void SaveGame(string path)
    {
        //Object[] q;
        //q = FindSceneObjectsOfType(typeof(GameObject));
        //for(int i = 0;i< q.Length;i++)
        //Debug.Log(q.GetValue(i));

        decomposer.levelModel.levelScore = score;
        decomposer.levelModel.levelTime = time;
        File.Delete(path);
        foreach (GameObject item in builder.level.LevelObjects)
        {
            if (item != null)
            {
                if (item.tag == "Wall")
                {
                    decomposer.levelModel.wallList.Add(decomposer.WallDecompose(item));
                }
                if (item.tag == "Enemy")
                {
                    decomposer.levelModel.meleeList.Add(decomposer.MeleeDecompose(item));
                }
                if (item.tag == "EnemyRange")
                {
                    decomposer.levelModel.rangeList.Add(decomposer.RangeDecompose(item));
                }
                if (item.tag == "Player")
                {
                    decomposer.levelModel.playerObj = decomposer.PlayerDecompose(item);
                }
            }
        }
       
        Serialiazer.SerialiazeToXml(ref decomposer.levelModel, path);
    }

    private void OnDestroy()
    {
        builder.level.LevelObjects.ForEach(o => { Destroy(o); });
        builder.level.LevelObjects.Clear();
    }
}
