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
    public Button chooseLevelText;
    public Button loadLevelText;
    public ScrollRect levelListText;
    public GameObject levelListContent;
    private List<GameObject> levelList;

    #endregion

    #region CommonMenuMembers

    public List<Button> closeButtons;
    private List<string> levelPathes;
    private List<string> userLevelPathes;
    private List<User> users = new List<User>();
    private string currentUser;
    private bool isLoadGamePressed;
    private ColorBlock buttonStyle = new ColorBlock() { normalColor = new Color32(187, 210, 83, 255), highlightedColor = new Color32(0, 0, 0, 255), pressedColor = new Color32(0, 0, 0, 255), fadeDuration = 0.4f, colorMultiplier = 1f };

    private void DirectoryCheck(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    //On click user updates current user
    private delegate void HighlightListTip(string text);

    private delegate void SwitchWindow(Canvas parent, Canvas current, bool enable);
    private delegate void SwitchMenuAndWindow(Canvas parent, Canvas current,List<GameObject> parentMenuComponents,bool enableCurrent, bool enableParent, bool enableComponents);
    private delegate void SwitchComponents(List<GameObject> components, List<bool> enableComponents);

    SwitchWindow switchWindowDelegate = (parent, current, enable) =>
    {
        current.enabled = !enable;
        parent.enabled = enable;
    };

    SwitchMenuAndWindow switchMenuAndWindowDelegate = (parent, current, components, enableParent, enableCurrent, enableComponents) =>
    {
        current.enabled = enableCurrent;
        parent.enabled = enableParent;
        foreach (GameObject item in components)
        {
            if (item.GetComponent<Button>() != null)
            {
                item.GetComponent<Button>().enabled = enableComponents;
            }
            if (item.GetComponent<ScrollRect>() != null)
            {
                item.GetComponent<ScrollRect>().enabled = enableComponents;
            }
        }
    };

    SwitchComponents switchComponentsDelegate = (components, enableComponents) =>
    {
        if(components.Count != enableComponents.Count)
        {
            throw new System.ArgumentException(string.Format("Quantity of arrays doesn't compiles: components.Count = {0}, enableComponents.Count = {1}", components.Count, enableComponents.Count));
        }
        int length = components.Count;
        for (int i = 0; i < length; i++)
        {
            if (components[i].GetComponent<Button>() != null)
            {
                components[i].GetComponent<Button>().enabled = enableComponents[i];
            }
            if (components[i].GetComponent<ScrollRect>() != null)
            {
                components[i].GetComponent<ScrollRect>().enabled = enableComponents[i];
            }
            if (components[i].GetComponent<Canvas>() != null)
            {
                components[i].GetComponent<Canvas>().enabled = enableComponents[i];
            }
        }
    };

    void Start ()
    {
        levelPathes = Directory.GetFiles(GlobalData.levelsDir).Select(s => s).OrderBy(s => s.Length).ThenBy(s => s, new StringComparer()).ToList();
        closeButtons[0].onClick.AddListener(() => switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, false, true));
        closeButtons[1].onClick.AddListener(() => switchWindowDelegate(addUserWindow, warningWindow, true));
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
                ScrollRectContentInit(userList, userListContent, "user", i, users[i].Name, 200);
            }

            currentUser = users[0].Name;
        }

        switchComponentsDelegate.Invoke(new List<GameObject>() { warningWindow.gameObject, addUserWindow.gameObject, quitMenu.gameObject, optionsMenu.gameObject, mainMenu.gameObject, continueMenu.gameObject, chooseProfileMenu.gameObject }, new List<bool>() { false, false, false, false, false, false, true });
    }

    #endregion

    #endregion

    #region MainMenu

    public void ExitPress()
    {
        switchMenuAndWindowDelegate(mainMenu, quitMenu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, true, false);
    }

    public void NewGamePress()
    {
        if(levelPathes.Count > 0)
        {
            GlobalData.levelName = levelPathes[0];
            SceneManager.LoadSceneAsync("Level");
        }
    }

    public void OptionsPress()
    {
        switchWindowDelegate.Invoke(optionsMenu, mainMenu, true);
    }

    public void ProfileChoosePress()
    {
        switchWindowDelegate.Invoke(chooseProfileMenu, mainMenu, true);
    }

    public void ContinuePress()
    {
        switchWindowDelegate.Invoke(continueMenu, mainMenu, true);
    }

    #endregion

    #region Options

    #endregion

    #region ChooseProfile

    //Enable adding user canvas
    public void CreateNewUserPress()
    {
        switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, true, false);
    }

    //Create user on add action
    public void CreateUserOnAdd()
    {
        if (!string.IsNullOrEmpty(inputField.text) && !users.Exists(x => x.Name.ToLower() == inputField.text.ToLower()))
        {
            int length = userList.Count;
            ScrollRectContentInit(userList, userListContent, "user", length, inputField.text, 200);

            users.Add(new User() { Name = inputField.text, LevelsAccess = new List<bool>(levelPathes.Select(s => false)), LevelsScore = new List<int>(levelPathes.Select(s => 0)) });
            Directory.CreateDirectory(string.Format("{0}\\{1}", GlobalData.usersDir, inputField.text));
            Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", GlobalData.dataDir, GlobalData.usersListFileName));
            switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, false, true);
            inputField.text = "";
        }
        else
        {
            switchWindowDelegate.Invoke(addUserWindow,warningWindow,false);
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
            switchWindowDelegate.Invoke(chooseProfileMenu, mainMenu, false);
            ClearLevelList();
            HelloText.text = string.Format("Hello, {0}!", currentUser);
        }
    }

    //Delete current user
    public void DeleteUserPress()
    {
        string userName = users.Find(item => item.Name == currentUser).Name;
        GameObject deleteObj = userList.Find(item => item.GetComponent<Text>().text == currentUser);
        Destroy(deleteObj);
        userList.Remove(deleteObj);
        users.Remove(users.Find(item => item.Name == userName));
        if (userList.Count == 0)
        {
            currentUser = "";
        }
        for (int i = 0; i < userList.Count; i++)
        {
            userList[i].GetComponent<RectTransform>().localPosition = new Vector3(0, -i * 50, 0);
        }
        Directory.Delete(string.Format("{0}\\{1}", GlobalData.usersDir, userName));
        Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", GlobalData.dataDir, GlobalData.usersListFileName));
    }

    #endregion

    #region Continue

    public void ChooseLevelPress()
    {
        LevelScrollRectContentInit(false, GlobalData.levelsDir, levelPathes);
    }

    public void LoadGamePress()
    {
        LevelScrollRectContentInit(true, GlobalData.usersDir + "\\" + currentUser, userLevelPathes);
    }

    private void LevelScrollRectContentInit(bool typeOfLoad,string directory, List<string> levelPathes)
    {
        isLoadGamePressed = typeOfLoad;
        ClearLevelList();
        for (int i = 0; i < levelPathes.Count; i++)
        {
            ScrollRectContentInit(levelList, levelListContent, "level", i, levelPathes[i].Replace(directory + "\\", "").Replace(".xml", ""), 700);
        }
    }

    #endregion

    #region Common

    private void ScrollRectContentInit(List<GameObject> list, GameObject content, string currentFlag, int pos, string text, int width)
    {
        list.Add(new GameObject());
        list[pos].AddComponent<Text>();
        list[pos].GetComponent<Text>().text = text;
        list[pos].GetComponent<Text>().font = Resources.Load<Font>(GlobalData.font);
        list[pos].GetComponent<Text>().fontSize = 30;
        list[pos].GetComponent<Text>().fontStyle = FontStyle.Normal;
        list[pos].GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        list[pos].GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        list[pos].GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;

        list[pos].AddComponent<Button>();
        list[pos].GetComponent<Button>().colors = buttonStyle;
        string tempStr = list[pos].GetComponent<Text>().text;
        HighlightListTip highlightListTipDelegate = (s) => { };
        switch(currentFlag)
        {
            case "user":
                {
                    highlightListTipDelegate = (s) =>
                    {
                        currentUser = s;
                    };
                    break;
                }
            case "level":
                {
                    highlightListTipDelegate = (s) =>
                    {
                        if (isLoadGamePressed)
                        {
                            GlobalData.levelName = GlobalData.usersDir + "\\" + currentUser;
                        }
                        else
                        {
                            GlobalData.levelName = GlobalData.levelsDir;
                        }
                        GlobalData.levelName += "\\" + s + ".xml";
                        if (File.Exists(GlobalData.levelName))
                        {
                            SceneManager.LoadSceneAsync("Level");
                        }
                        else
                        {
                            //warningText.text = string.Format("File {0} does not exist", GlobalData.levelName);
                        }
                    };
                    break;
                }
        }
        list[pos].GetComponent<Button>().onClick.AddListener(() => highlightListTipDelegate(tempStr));

        list[pos].transform.SetParent(content.transform);
        list[pos].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().sizeDelta = new Vector2(width, 50);
        list[pos].GetComponent<RectTransform>().localPosition = new Vector3(0, -pos * 50, 0);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (pos + 1) * 52.5f);
    }

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
        switchMenuAndWindowDelegate(mainMenu, menu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, false, true);
    }

    public void Debug()
    {
        SceneManager.LoadSceneAsync("TestingArea");
    }

    #endregion
}
