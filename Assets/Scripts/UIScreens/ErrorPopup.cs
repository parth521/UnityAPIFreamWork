
using System;
using UnityEngine;
using TMPro;
public class ErrorPopup : UIElement
{
    [SerializeField]private ErrorMessages errorMessages;
    [SerializeField] private TextMeshProUGUI errorMessagetxt;
    public override void Show(Action callback = null)
    {
        errorMessagetxt.text=errorMessages.GetErrorMessage(errorMessages.currentErrorType);
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
