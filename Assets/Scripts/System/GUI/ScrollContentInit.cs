using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilites.Serialiazer;

class ScrollContentInit
{
    public delegate void HighlightListTip(string text);

    public static void ScrollContentInitialize(List<GameObject> list, GameObject content, int pos, string text, int width, ColorBlock buttonStyle, HighlightListTip highlightListTipDelegate)
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
        
        list[pos].GetComponent<Button>().onClick.AddListener(() => highlightListTipDelegate(tempStr));

        list[pos].transform.SetParent(content.transform);
        list[pos].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        list[pos].GetComponent<RectTransform>().sizeDelta = new Vector2(width, 50);
        list[pos].GetComponent<RectTransform>().localPosition = new Vector3(0, -pos * 50, 0);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (pos + 1) * 52.5f);
    }

    public static void DeleteContentObject<T>(List<T> list, T listCurrentObject,ref string objectText, List<GameObject> contentObjectsList)
    {
        string objTextTemp = objectText;
        GameObject deleteObj = contentObjectsList.Find(item => item.GetComponent<Text>().text == objTextTemp);
        UnityEngine.Object.Destroy(deleteObj);
        contentObjectsList.Remove(deleteObj);
        list.Remove(listCurrentObject);
        if (contentObjectsList.Count == 0)
        {
            objectText = "";
        }
        for (int i = 0; i < contentObjectsList.Count; i++)
        {
            contentObjectsList[i].GetComponent<RectTransform>().localPosition = new Vector3(0, -i * 50, 0);
        }
    }
}
