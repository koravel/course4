using UnityEngine;
using UnityEngine.UI;
using Utilites.Level;
using Utilites.Serialiazer;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas quitMenu;
    public Button exitMainMenuText;
    public Button startText;
    public Button continueText;
    public Button profileChangeText;
    public Button optionsText;
    public Text HelloText;

    public Canvas optionsMenu;

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

    public Canvas levelsMenu;

    public List<Button> closeButtons;
    private Level currentLevel = new Level();
    private List<string> levelPathes;
    private List<User> users = new List<User>();
    private string currentUser;
    private string usersDir = "Users";
    private string dataDir = "Data";
    private string usersListFileName = "users.xml";
    private string levelsListFileName = "levels.xml";

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
        closeButtons[0].onClick.AddListener(() => switchMenuAndWindowDelegate(chooseProfileMenu, addUserWindow, new List<GameObject>() { createNewUserText.gameObject, chooseUserText.gameObject, deleteUserText.gameObject, exitProfileText.gameObject, userListText.gameObject }, true, false, true));
        closeButtons[1].onClick.AddListener(() => switchWindowDelegate(addUserWindow, warningWindow, true));
        userList = new List<GameObject>();
        currentUser = "";
        DirectoryCheck(dataDir);
        DirectoryCheck(usersDir);
        if (File.Exists(dataDir + "\\" + usersListFileName))
        {
            Serialiazer.DeserialiazitionFromXml(ref users,string.Format("{0}\\{1}", dataDir, usersListFileName));
        }
        if (File.Exists(dataDir + "\\" + levelsListFileName))
        {
            Serialiazer.DeserialiazitionFromXml(ref levelPathes, string.Format("{0}\\{1}", dataDir, levelsListFileName));
        }
        if (users.Count > 0)
        {
            for (int i = 0; i < users.Count; i++)
            {
                OnUserCreate(i, users[i].Name);
            }

            currentUser = users[0].Name;
        }

        switchComponentsDelegate.Invoke(new List<GameObject>() { warningWindow.gameObject, addUserWindow.gameObject, quitMenu.gameObject, optionsMenu.gameObject, mainMenu.gameObject, levelsMenu.gameObject, chooseProfileMenu.gameObject }, new List<bool>() { false, false, false, false, false, false, true });
    }

    #region MainMenu

    public void ExitPress()
    {
        switchMenuAndWindowDelegate(mainMenu, quitMenu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, true, false);
    }

    public void NewGamePress()
    {
        currentLevel.LevelName = "TestingArea";
        currentLevel.Load();
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
        switchWindowDelegate.Invoke(levelsMenu, mainMenu, true);
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
            OnUserCreate(length, inputField.text);
            users.Add(new User() { Name = inputField.text, LevelsAccess = new List<bool>() {  }, LevelsScore = new List<int>() {  } });
            Directory.CreateDirectory(string.Format("{0}\\{1}", usersDir, inputField.text));
            Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", dataDir, usersListFileName));
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
            switchWindowDelegate.Invoke(chooseProfileMenu, mainMenu, false);
            HelloText.text = string.Format("Hello, {0}!", currentUser);
        }
    }
    //Create user function
    private void OnUserCreate(int pos,string text)
    {
        userList.Add(new GameObject());
        userList[pos].AddComponent<Text>();
        userList[pos].GetComponent<Text>().text = text;
        userList[pos].GetComponent<Text>().font = Resources.Load<Font>("Fonts\\JazzCreateBubble");
        userList[pos].GetComponent<Text>().fontSize = 30;
        userList[pos].GetComponent<Text>().fontStyle = FontStyle.Normal;
        userList[pos].GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        userList[pos].GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        userList[pos].GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;

        userList[pos].AddComponent<Button>();
        userList[pos].GetComponent<Button>().colors = new ColorBlock() { normalColor = new Color32(187, 210, 83, 255), highlightedColor = new Color32(0, 0, 0, 255), pressedColor = new Color32(0, 0, 0, 255), fadeDuration = 0.4f, colorMultiplier = 1f };
        string tempStr = userList[pos].GetComponent<Text>().text;
        HighlightListTip highlightListTipDelegate = (s) => 
        {
            currentUser = s;
        };
        userList[pos].GetComponent<Button>().onClick.AddListener(() => highlightListTipDelegate(tempStr));

        userList[pos].transform.SetParent(userListContent.transform);
        userList[pos].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        userList[pos].GetComponent<RectTransform>().localPosition = new Vector3(0, -pos * 50, 0);
        userListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (pos + 1) * 52.5f);
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
        Directory.Delete(string.Format("{0}\\{1}", usersDir, userName));
        Serialiazer.SerialiazeToXml(ref users, string.Format("{0}\\{1}", dataDir, usersListFileName));
    }

    #endregion

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenuPress(Canvas menu)
    {
        switchMenuAndWindowDelegate(mainMenu, menu, new List<GameObject>() { startText.gameObject, exitMainMenuText.gameObject, continueText.gameObject, profileChangeText.gameObject, optionsText.gameObject }, true, false, true);
    }
}
