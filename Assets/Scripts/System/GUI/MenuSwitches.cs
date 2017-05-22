using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitches
{
    public delegate void SwitchWindow(Canvas parent, Canvas current, bool enable);
    public delegate void SwitchMenuAndWindow(Canvas parent, Canvas current, List<GameObject> parentMenuComponents, bool enableCurrent, bool enableParent, bool enableComponents);
    public delegate void SwitchComponents(List<GameObject> components, List<bool> enableComponents);

    public static SwitchWindow switchWindowDelegate = (parent, current, enable) =>
    {
        current.enabled = !enable;
        parent.enabled = enable;
    };

    public static SwitchMenuAndWindow switchMenuAndWindowDelegate = (parent, current, components, enableParent, enableCurrent, enableComponents) =>
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
            if(item.GetComponent<Slider>() != null)
            {
                item.GetComponent<Slider>().enabled = enableComponents;
            }
        }
    };

    public static SwitchComponents switchComponentsDelegate = (components, enableComponents) =>
    {
        if (components.Count != enableComponents.Count)
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
}
