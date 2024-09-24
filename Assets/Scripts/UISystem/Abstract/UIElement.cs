using System;
using UnityEngine;
public abstract class UIElement : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public UIScreen screenName;

    private void Awake()
    {

        canvas = GetComponent<Canvas>();
        canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found in child");
        }
        UIManager.Instance.RegisterUIElements(this);
    }
    public virtual void Show(Action callback = null)
    {
        canvas.enabled = true;
        callback?.Invoke();
        canvasGroup.blocksRaycasts = true;
    }
    public void HideDirectly()
    {
        canvasGroup.blocksRaycasts = false;
        canvas.enabled = false;
    }
    public virtual void Hide(Action callback = null)
    {
        canvas.enabled = false;
        callback?.Invoke();
    }
    public void SetLayerOder(int order)
    {
        canvas.sortingOrder = order;
    }
}
