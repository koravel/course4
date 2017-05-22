using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class InGameMenu : MonoBehaviour
{

    #region Members

    #region InGameMenuMembers

    public Canvas inGameMenu;
    public Button pauseButton;
    public Text scoreText;
    public Text timeText;

    #endregion

    #region PauseMenuMembers

    public Canvas pauseMenu;
    public Button remuseText;
    public Button loadAndSaveGameText;
    public Button backToMainMenuText;
    public Button quitText;

    #endregion

    #region LevelManagerMenuMembers

    public Canvas levelManagerMenu;
    public Button saveLevelText;
    public Button loadLevelText;
    public Button deleteLevelText;
    public Button backToPauseMenuText;
    public Slider loadViewSlider;
    public GameObject levelsScrollContent;
    public Canvas saveLevelCanvas;
    public InputField saveLevelInput;
    public Button saveConfirm;
    public Button saveEscape;
    private List<GameObject> levelListContent;
    private List<string> userLevelPathes;
    private string currentLevel;
    private int currentLevelIndex;
    private string currentLevelDir;

    #endregion

    #region CommonMembers

    private ColorBlock buttonStyle = new ColorBlock() { normalColor = new Color32(187, 210, 83, 255), highlightedColor = new Color32(0, 0, 0, 255), pressedColor = new Color32(0, 0, 0, 255), disabledColor = new Color32(0, 0, 0, 255), fadeDuration = 0.4f, colorMultiplier = 1f };
    private int time;
    private int beginLevelTyme;
    public BuilderDirector director;

    void Start()
    {
        Time.timeScale = 1;
        timeText.text = "00:00:00";
        beginLevelTyme = Mathf.RoundToInt(Time.time);
        ScoreUpdate(0);
        GlobalData.score = 0;
        levelListContent = new List<GameObject>();
        MenuSwitches.switchComponentsDelegate.Invoke(new List<GameObject>() { pauseMenu.gameObject, levelManagerMenu.gameObject, inGameMenu.gameObject, pauseButton.gameObject, scoreText.gameObject, timeText.gameObject, saveLevelCanvas.gameObject }, new List<bool>() { false, false, true, true, true, true, false });
    }

    private string TimeStyleCheck(int number)
    {
        return number < 10 ? "0" + number : number.ToString();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !levelManagerMenu.enabled)
        {
            if (pauseButton.enabled)
            {
                PausePressed();
            }
            else
            {
                RemusePressed();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > (time + 1))
        {
            time = Mathf.RoundToInt(Time.time) - beginLevelTyme;
            int hours = (time + director.time) / 3600;
            int minutes = (time + director.time) / 60 - hours * 60;
            int seconds = (time + director.time) - minutes * 60 - hours * 3600;
            if (hours > 99)
            {
                timeText.text = "Are'u f*cking kidding me?";
            }
            else
            {
                timeText.text = string.Format("{0}:{1}:{2}", TimeStyleCheck(hours), TimeStyleCheck(minutes), TimeStyleCheck(seconds));
            }
        }
        ScoreUpdate(GlobalData.score + director.score);
    }

    #endregion

    #endregion

    #region InGameMenu

    public void ScoreUpdate(int scoreValue)
    {
        scoreText.text = scoreValue.ToString();
    }

    public void PausePressed()
    {
        Time.timeScale = 0;
        FindObjectsOfType(typeof(GameObject)).ToList().ForEach(o => { (o as GameObject).SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver); });
        MenuSwitches.switchComponentsDelegate.Invoke(new List<GameObject>() { pauseButton.gameObject, pauseMenu.gameObject}, new List<bool>() { false, true });
    }

    public void ReloadLevelPressed()
    {
        SceneManager.LoadSceneAsync("Level");
    }

    #endregion

    #region PauseMenu

    public void RemusePressed()
    {
        Time.timeScale = 1;
        FindObjectsOfType(typeof(GameObject)).ToList().ForEach(o => { (o as GameObject).SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver); });
        MenuSwitches.switchComponentsDelegate.Invoke(new List<GameObject>() { pauseButton.gameObject, pauseMenu.gameObject }, new List<bool>() { true, false });
    }

    public void LoadAndSavePressed()
    {
        MenuSwitches.switchWindowDelegate(pauseMenu, levelManagerMenu, false);
        SliderStateChange();
    }

    private void ScrollContentFill(List<string> list, string directory)
    {
        ClearLevelList();
        for (int i = 0; i < list.Count; i++)
        {
            int tempPos = i;
            ScrollContentInit.ScrollContentInitialize(levelListContent, levelsScrollContent, i, list[i].Replace(directory + "\\", "").Replace(".xml", ""), 700, buttonStyle, (s) => 
            {
                currentLevelDir = directory;
                currentLevelIndex = tempPos;
                currentLevel = s;
            });
            if (!GlobalData.user.LevelsAccess[i] && loadViewSlider.value == 1)
            {
                levelListContent[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    public void BackToMainMenuPressed()
    {
        GlobalData.score = 0;
        GlobalData.levelName = "";
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        
    }

    public void QuitGamePressed()
    {
        Application.Quit();
    }

    #endregion

    #region LevelManagerMenu

    public void SaveLevelPress()
    {
        MenuSwitches.switchMenuAndWindowDelegate(levelManagerMenu, saveLevelCanvas, new List<GameObject>() { saveLevelText.gameObject, loadLevelText.gameObject, deleteLevelText.gameObject, backToPauseMenuText.gameObject, loadViewSlider.gameObject }, true, true, false);
    }

    public void SaveEscape()
    {
        MenuSwitches.switchMenuAndWindowDelegate(levelManagerMenu, saveLevelCanvas, new List<GameObject>() { saveLevelText.gameObject, loadLevelText.gameObject, deleteLevelText.gameObject, backToPauseMenuText.gameObject, loadViewSlider.gameObject }, true, false, true);
    }

    public void SaveConfirm()
    {
        director.score = int.Parse(scoreText.text);
        director.time = time;
        director.SaveGame(GlobalData.usersDir + "\\" + GlobalData.user.Name + "\\" + saveLevelInput.text + ".xml");
        MenuSwitches.switchMenuAndWindowDelegate(levelManagerMenu, saveLevelCanvas, new List<GameObject>() { saveLevelText.gameObject, loadLevelText.gameObject, deleteLevelText.gameObject, backToPauseMenuText.gameObject, loadViewSlider.gameObject }, true, false, true);
        SliderStateChange();
        director.score = 0;
        director.time = 0;
    }

    public void DeleteLevelPressed()
    {
        ScrollContentInit.DeleteContentObject(userLevelPathes, userLevelPathes.Find(item => item == currentLevel), ref currentLevel, levelListContent);

        string path = currentLevelDir + "\\" + currentLevel + ".xml";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void LoadLevelPress()
    {
        GlobalData.levelName = currentLevelDir + "\\" + currentLevel + ".xml";
        if (File.Exists(GlobalData.levelName))
        {
            if (loadViewSlider.value == 0)
            {
                SceneManager.LoadSceneAsync("Level");
            }
            else
            {
                if (GlobalData.user.LevelsAccess[currentLevelIndex])
                {
                    SceneManager.LoadSceneAsync("Level");
                }
            }
        }
    }

    public void SliderStateChange()
    {
        currentLevel = "";
        string path = GlobalData.usersDir + "\\" + GlobalData.user.Name;
        userLevelPathes = Directory.GetFiles(path).Select(s => s).OrderBy(s => s.Length).ThenBy(s => s, new StringComparer()).ToList();
        if (loadViewSlider.value == 0)
        {
            saveLevelText.enabled = true;
            deleteLevelText.enabled = true;
            ScrollContentFill(userLevelPathes, path);
        }
        else
        {
            saveLevelText.enabled = false;
            deleteLevelText.enabled = false;
            ScrollContentFill(GlobalData.levelPathes, GlobalData.levelsDir);
        }
    }

    public void BackToPauseMenu()
    {
        MenuSwitches.switchWindowDelegate(levelManagerMenu, pauseMenu, false);
    }

    #endregion

    #region Common

    private void ClearLevelList()
    {
        if (levelListContent.Count != 0)
        {
            levelListContent.ForEach((o) => { Destroy(o); });
            levelListContent.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        App.UsersSave();
    }

    #endregion

}
