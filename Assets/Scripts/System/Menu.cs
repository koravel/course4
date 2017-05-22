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

    void Start ()
    {
        addUserWindow.enabled = false;
        userList = new List<GameObject>();
        currentUser = "";
        DirectoryCheck(dataDir);
        DirectoryCheck(usersDir);
        if (File.Exists(dataDir + "\\" + usersListFileName))
        {
            Serialiazer.DeserialiazitionFromXml(ref users, dataDir + "\\" + usersListFileName);
        }
        if (File.Exists(dataDir + "\\" + levelsListFileName))
        {
            Serialiazer.DeserialiazitionFromXml(ref levelPathes, dataDir + "\\" + levelsListFileName);
        }
        if (users.Count > 0)
        {
            for (int i = 0; i < users.Count; i++)
            {
                OnUserCreate(i, users[i].Name);
            }

            currentUser = users[0].Name;
        }

        quitMenu.enabled = false;
        optionsMenu.enabled = false;
        mainMenu.enabled = false;
        levelsMenu.enabled = false;
        chooseProfileMenu.enabled = true;
    }

    #region MainMenu

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitMainMenuText.enabled = false;
        continueText.enabled = false;
        profileChangeText.enabled = false;
        optionsText.enabled = false;
    }

    public void NewGamePress()
    {
        currentLevel.LevelName = "TestingArea";
        currentLevel.Load();
    }

    public void OptionsPress()
    {
        mainMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void ProfileChoosePress()
    {
        mainMenu.enabled = false;
        chooseProfileMenu.enabled = true;
    }

    public void ContinuePress()
    {
        mainMenu.enabled = false;
        levelsMenu.enabled = true;
    }

    #endregion

    #region Options

    #endregion

    #region ChooseProfile
    //Enable adding user canvas
    public void CreateNewUserPress()
    {
        addUserWindow.enabled = true;
        createNewUserText.enabled = false;
        chooseUserText.enabled = false;
        deleteUserText.enabled = false;
        exitProfileText.enabled = false;
        userListText.enabled = false;
    }

    //Create user on add action
    public void CreateUserOnAdd()
    {
        if (!string.IsNullOrEmpty(inputField.text) && !users.Exists(x => x.Name.ToLower() == inputField.text.ToLower()))
        {
            int length = userList.Count;
            OnUserCreate(length, inputField.text);
            users.Add(new User() { Name = inputField.text, LevelsAccess = new List<bool>() {  }, LevelsScore = new List<int>() {  } });
            Directory.CreateDirectory(usersDir + "\\" + inputField.text);
            Serialiazer.SerialiazeToXml(ref users, dataDir + "\\" + usersListFileName);
            addUserWindow.enabled = false;
            inputField.text = "";
            createNewUserText.enabled = true;
            chooseUserText.enabled = true;
            deleteUserText.enabled = true;
            exitProfileText.enabled = true;
            userListText.enabled = true;
        }
        else
        {
            addUserWindow.enabled = false;
            warningText.text = "Name is empty or already exists";
            warningWindow.enabled = true;
        }
    }

    public void WarningNamePress()
    {
        warningWindow.enabled = false;
        addUserWindow.enabled = true;
    }

    //Choose user and go to main menu
    public void ChooseUserPress()
    {
        if(!string.IsNullOrEmpty(currentUser))
        {
            chooseProfileMenu.enabled = false;
            HelloText.text = "Hello, " + currentUser + "!";

            mainMenu.enabled = true;
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
        HighlightListTip hlt = (s) => 
        {
            currentUser = s;
        };
        userList[pos].GetComponent<Button>().onClick.AddListener(() => hlt(tempStr));

        userList[pos].transform.SetParent(userListContent.transform);
        userList[pos].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        userList[pos].GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        userList[pos].GetComponent<RectTransform>().localPosition = new Vector3(0, -pos * 50, 0);
        userListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (pos + 1) * 52.5f);
    }

    //On click user updates current user
    delegate void HighlightListTip(string text);

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
        Directory.Delete(usersDir + "\\" + userName);
        Serialiazer.SerialiazeToXml(ref users, dataDir + "\\" + usersListFileName);
    }

    #endregion

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenuPress(Canvas menu)
    {
        mainMenu.enabled = true;
        menu.enabled = false;
        startText.enabled = true;
        exitMainMenuText.enabled = true;
        continueText.enabled = true;
        profileChangeText.enabled = true;
        optionsText.enabled = true;

    }
}
