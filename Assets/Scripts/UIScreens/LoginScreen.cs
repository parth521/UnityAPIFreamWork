using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class LoginScreen : UIElement
{
    [Header("API")]
    [SerializeField] private LoginAPI loginAPI;

    [Header("UI Elements")]
    [SerializeField] private TMP_InputField studentIdentifierInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private ErrorMessages errorMessages;
    public override void Show(Action callback = null) {
        base.Show();
        loginAPI.OnSuccessActions.Add(OnLoginSuccess);
        loginAPI.OnFailedActions.Add(OnLoginFailed);
        loginButton.onClick.AddListener(OnLogin);
    }
    public override void Hide(Action callback = null) {
        base.Hide();
        loginButton.onClick.RemoveListener(OnLogin);
        loginAPI.OnSuccessActions.Remove(OnLoginSuccess);
        loginAPI.OnFailedActions.Remove(OnLoginFailed);
    }
    private void OnLogin()
    { 
        UIManager.Instance.ShowScreen(UIScreen.loadingScreen);
        WWWForm form = new WWWForm();
        form.AddField("student_identifier", studentIdentifierInputField.text);
        form.AddField("password", passwordInputField.text);
        form.AddField("device_name", SystemInfo.deviceName);
        loginAPI.PostRequest(form);
    }
    private void OnLoginSuccess()
    {
        UIManager.Instance.HideScreen(UIScreen.loadingScreen);
        UIManager.Instance.ShowScreen(UIScreen.levelSelectionScreen);
    }
    private void OnLoginFailed()
    {
        UIManager.Instance.HideScreen(UIScreen.loadingScreen);
        errorMessages.currentErrorType = ErrorType.UnauthorizedAccess;
        UIManager.Instance.ShowScreen(UIScreen.errorPopup);
    }
}