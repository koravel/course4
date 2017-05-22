using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject inGameMenu;

    #region GameControllerMembers

    public Canvas winGameCanvas;
    public Canvas looseGameCanvas;
    public Text scoreText;
    public Text nextLevelText;
    public BuilderDirector director;
    private bool actionFlag;
    private float time;
    #endregion

    void Start ()
    {
        actionFlag = false;
        time = Time.time;
        if (GlobalData.user.LevelsAccess.Count <= GlobalData.levelIndex + 1)
        {
            nextLevelText.text = "The end.";
            nextLevelText.gameObject.GetComponent<Button>().enabled = false;
            nextLevelText.transform.parent.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (!actionFlag)
        {
            time = Time.time + 3;
        }
        if (director.enemies.Count == 0)
        {
            actionFlag = true;
            if (Time.time > time)
            {
                winGameCanvas.enabled = true;
            }
        }
        else if (director.player == null)
        {
            actionFlag = true;
            if (Time.time > time + 3)
            {
                looseGameCanvas.enabled = true;
            }
        }
    }

    #region WinGame

    public void NextLevelPressed()
    {
        GlobalData.levelIndex++;
        if (GlobalData.user.LevelsAccess.Count > GlobalData.levelIndex)
        {
            GlobalData.user.LevelsAccess[GlobalData.levelIndex] = true;
            GlobalData.user.LevelsScore[GlobalData.levelIndex] = int.Parse(scoreText.text);
            GlobalData.levelName = GlobalData.levelPathes[GlobalData.levelIndex];
            SceneManager.LoadSceneAsync("Level");
        }
    }

    #endregion

}
