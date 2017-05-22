using UnityEngine;
using UnityEngine.UI;
using Utilites.Serialiazer;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu : MonoBehaviour
{

    #region Members

    #region MainMenuMembers

    public Canvas mainMenu;
    public Canvas quitMenu;
    public Button exitMainMenuText;
    public Button startText;
    public Button continueText;
    public Button profileChangeText;
    public Button optionsText;
    public Text HelloText;

    #endregion

    #region OptionsMenuMembers

    public Canvas optionsMenu;

    #endregion

    #region ChooseProfileMenuMembers

    public Canvas chooseProfileMenu;
    public Canvas addUserWindow;
    public Canvas warningWindow;
    public Text warningText;
    public Image warningBackround;
    public Button createNewUserText;
    public Button chooseUserText;
    public Button deleteUserText;
    public Button exitProfileText;
    public ScrollRect userListText;
    public GameObject userListContent;
    public InputField inputField;
    private List<GameObject> userList;

    #endregion

    #region ContinueMenuMembers

    public Canvas continueMenu;
    public Button loadLevelText;
    public Button deleteLevelText;
    public Slider loadViewSlider;
    public ScrollRect levelListText;
    public GameObject levelListContent;
    private List<GameObject> levelList;
    private string currentLevel;
    private int currentLevelIndex;
    private string currentLevelDir;

    #endregion

    #region CommonMenuMembers

    public List<Button> closeButtons;
    private List<string> userLevelPathes;
    private List<User> users = new List<User>();
    private string currentUser;
    private bool isLoadGamePressed;
    private ColorBlock buttonStyle = new ColorBlock() { normalColor = new Color32(187, 210, 83, 255), highlightedColor = new Color32(0, 0, 0, 255), pressedColor = new Color32(0, 0, 0, 255), disabledColor = new Color32(0, 0, 0, 255), fadeDuration = 0.4f, colorMultiplier = 1f };

    private void DirectoryCheck(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    void Start ()
    {
        GlobalData.levelPathes = Directory.GetFiles(GlobalData.levelsDir).Select(s => s).OrderBy(s => s.Length).ThenBy(s => s, new StringComparer()).ToList();
        closeButtons[0].onClick.AddListener(() => MenuSwitches.switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, false, true));
        closeButtons[1].onClick.AddListener(() => MenuSwitches.switchWindowDelegate(addUserWindow, warningWindow, true));
        userList = new List<GameObject>();
        levelList = new List<GameObject>();
        currentUser = "";
        DirectoryCheck(GlobalData.dataDir);
        DirectoryCheck(GlobalData.usersDir);
        string pathUserData = string.Format("{0}\\{1}", GlobalData.dataDir, GlobalData.usersListFileName);
        if (File.Exists(pathUserData))
        {
            Serialiazer.DeserialiazitionFromXml(ref users, pathUserData);
        }
        if (users.Count > 0)
        {
            for (int i = 0; i < users.Count; i++)
            {
                ScrollContentInit.ScrollContentInitialize(userList, userListContent, i, users[i].Name, 200, buttonStyle, (s) => { currentUser = s; });
            }

            currentUser = users[0].Name;
        }
        if(!GlobalData.isEntered)
        {
            MenuSwitches.switchComponentsDelegate.Invoke(new List<GameObject>() { warningWindow.gameObject, addUserWindow.gameObject, quitMenu.gameObject, optionsMenu.gameObject, mainMenu.gameObject, continueMenu.gameObject, chooseProfileMenu.gameObject }, new List<bool>() { false, false, false, false, false, false, true });
            GlobalData.isEntered = true;
        }
        else
        {
            currentUser = GlobalData.user.Name;
            HelloText.text = string.Format("Hello, {0}!", currentUser);
            userLevelPathes = Directory.GetFiles(string.Format("{0}\\{1}", GlobalData.usersDir, currentUser)).Select(s => s).OrderBy(s => s.Length).ThenBy(s => s, new StringComparer()).ToList();
            MenuSwitches.switchComponentsDelegate.Invoke(new List<GameObject>() { warningWindow.gameObject, addUserWindow.gameObject, quitMenu.gameObject, optionsMenu.gameObject, mainMenu.gameObject, continueMenu.gameObject, chooseProfileMenu.gameObject }, new List<bool>() { false, false, false, false, true, false, false });
        }
    }

    #endregion

    #endregion

    #region MainMenu

    public void ExitPress()
    {
        MenuSwitches.switchMenuAndWindowDelegate(mainMenu, quitMenu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, true, false);
    }

    public void NewGamePress()
    {
        if(GlobalData.levelPathes.Count > 0)
        {
            GlobalData.levelName = GlobalData.levelPathes[0];
            GlobalData.user.LevelsAccess[0] = true;
            SceneManager.LoadSceneAsync("Level");
        }
    }

    public void OptionsPress()
    {
        MenuSwitches.switchWindowDelegate.Invoke(optionsMenu, mainMenu, true);
    }

    public void ProfileChoosePress()
    {
        MenuSwitches.switchWindowDelegate.Invoke(chooseProfileMenu, mainMenu, true);
    }

    public void ContinuePress()
    {
        MenuSwitches.switchWindowDelegate.Invoke(continueMenu, mainMenu, true);
        SliderStateChange();
    }

    #endregion

    #region Options

    #endregion

    #region ChooseProfile

    //Enable adding user canvas
    public void CreateNewUserPress()
    {
        MenuSwitches.switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, true, false);
    }

    //Create user on add action
    public void CreateUserOnAdd()
    {
        if (!string.IsNullOrEmpty(inputField.text) && !users.Exists(x => x.Name.ToLower() == inputField.text.ToLower()))
        {
            int length = userList.Count;
            ScrollContentInit.ScrollContentInitialize(userList, userListContent, length, inputField.text, 200, buttonStyle, (s) => { currentUser = s; });

            users.Add(new User() { Name = inputField.text, LevelsAccess = new List<bool>(GlobalData.levelPathes.Select(s => false)), LevelsScore = new List<int>(GlobalData.levelPathes.Select(s => 0)) });
            Directory.CreateDirectory(string.Format("{0}\\{1}", GlobalData.usersDir, inputField.text));
            Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", GlobalData.dataDir, GlobalData.usersListFileName));
            MenuSwitches.switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, false, true);
            inputField.text = "";
        }
        else
        {
            MenuSwitches.switchWindowDelegate.Invoke(addUserWindow,warningWindow,false);
            warningText.text = "Name is empty or already exists";
        }
    }

    //Choose user and go to main menu
    public void ChooseUserPress()
    {
        if(!string.IsNullOrEmpty(currentUser))
        {
            GlobalData.user = users.Find(u => u.Name == currentUser);
            string curDir = string.Format("{0}\\{1}",GlobalData.usersDir ,currentUser);
            userLevelPathes = Directory.GetFiles(curDir).Select(s => s).OrderBy(s => s.Length).ThenBy(s => s, new StringComparer()).ToList();
            MenuSwitches.switchWindowDelegate.Invoke(chooseProfileMenu, mainMenu, false);
            ClearLevelList();
            HelloText.text = string.Format("Hello, {0}!", currentUser);
        }
    }

    //Delete current user
    public void DeleteUserPress()
    {

        ScrollContentInit.DeleteContentObject(users, users.Find(item => item.Name == currentUser), ref currentUser, userList);
        string path = string.Format("{0}\\{1}", GlobalData.usersDir, currentUser);
        Directory.GetFiles(path).ToList().ForEach(s => File.Delete(s));
        Directory.Delete(path);
        Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", GlobalData.dataDir, GlobalData.usersListFileName));
    }

    #endregion

    #region Continue

    public void DeleteLevelPressed()
    {
        ScrollContentInit.DeleteContentObject(userLevelPathes, userLevelPathes.Find(item => item == currentLevel), ref currentLevel, levelList);

        string path = currentLevelDir + "\\" + currentLevel + ".xml";

        if(File.Exists(path))
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
            deleteLevelText.enabled = true;
            LevelScrollRectContentInit(path, userLevelPathes);
        }
        else
        {
            deleteLevelText.enabled = false;
            LevelScrollRectContentInit(GlobalData.levelsDir, GlobalData.levelPathes);
        }
    }

    private void LevelScrollRectContentInit(string directory, List<string> levelPathes)
    {
        ClearLevelList();
        for (int i = 0; i < levelPathes.Count; i++)
        {
            int tempPos = i;
            ScrollContentInit.ScrollContentInitialize(levelList, levelListContent, i, levelPathes[i].Replace(directory + "\\", "").Replace(".xml", ""), 700, buttonStyle, (s) =>
            {
                currentLevelDir = directory;
                currentLevelIndex = tempPos;
                currentLevel = s;
            });
            if(!GlobalData.user.LevelsAccess[i] && loadViewSlider.value == 1)
            {
                levelList[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    #endregion

    #region Common
    
    private void ClearLevelList()
    {
        if (levelList.Count != 0)
        {
            levelList.ForEach((o) => { Destroy(o); });
            levelList.Clear();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenuPress(Canvas menu)
    {
        MenuSwitches.switchMenuAndWindowDelegate(mainMenu, menu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, false, true);
    }

    public void Debug()
    {
        SceneManager.LoadSceneAsync("TestingArea");
    }

    private void OnApplicationQuit()
    {
        App.UsersSave();
    }

    #endregion
}
