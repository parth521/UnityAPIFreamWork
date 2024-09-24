
using System;
public class ErrorPopup : UIElement
{
    public override void Show(Action callback = null)
    {
        base.Show();
    }
    public override void Hide(Action callback = null)
    {
        base.Hide();
    }
    public void HideScreen()
    {
       Hide ();
    }
}
