
using System.Collections.Generic;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIScreen initialScreen;
    [SerializeField] private List<UIElement> uiElements;
    private Stack<UIElement> screenQueue = new Stack<UIElement>();
    private void Start()
    {
        foreach (UIElement uiElement in uiElements)
        {
            uiElement.HideDirectly();
        }
        ShowScreen(initialScreen);
    }
    public void RegisterUIElements(UIElement uIElement)
    {
        if (!uiElements.Contains(uIElement))
        {
            uiElements.Add(uIElement);
        }
    }
    private void OnDestroy()
    {
        uiElements.Clear();
    }
    public void ShowScreen(UIScreen screen)
    {
        UIElement currentScreen = uiElements.Find(x => x.screenName == screen);
        if (currentScreen != null)
        {
            screenQueue.Push(currentScreen);
            currentScreen.Show();

            currentScreen.SetLayerOder(screenQueue.Count);
        }
    }
    public void HideScreen(UIScreen screen)
    {
        UIElement element = screenQueue.Pop();

        element.Hide(() =>
        {
            element.SetLayerOder(0);
        });
    }
    public void ChangeScreen(UIScreen screen)
    {
        if (screenQueue.Count > 0)
        {
            UIElement currentScreen = screenQueue.Pop();
            currentScreen.SetLayerOder(0);

            currentScreen.Hide(() =>
            {
                currentScreen = uiElements.Find(x => x.screenName == screen);
                if (currentScreen != null)
                {
                    currentScreen.Show();
                    screenQueue.Push(currentScreen);
                    currentScreen.SetLayerOder(screenQueue.Count);
                }
            });
        }
        else
        {
            ShowScreen(screen);
        }
    }
}
