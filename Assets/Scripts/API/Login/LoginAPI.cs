using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "API", menuName = "API/LoginAPI", order = 1)]
public class LoginAPI : BaseAPI
{
    [SerializeField] private LoginResponceData loginResponceData;
    private void OnEnable()
    {
        OnSuccessActions = new System.Collections.Generic.List<System.Action>();
        OnFailedActions = new System.Collections.Generic.List<System.Action>();
        OnUnAuthorizedActions = new System.Collections.Generic.List<System.Action>();
    }
    public void AddsuccesAction(System.Action action)
    {
        OnSuccessActions.Add(action);
    }
    public void AddFailedAction(System.Action action)
    {
        OnFailedActions.Add(action);
    }
    public void AddUnAuthorizedAction(System.Action action)
    {
        OnUnAuthorizedActions.Add(action);
    }
    public void RemoveSuccessAction(System.Action action)
    {
        OnSuccessActions.Remove(action);
    }
    public void RemoveFailedAction(System.Action action)
    {
        OnFailedActions.Remove(action);
    }
    public void RemoveUnAuthorizedAction(System.Action action)
    {
        OnUnAuthorizedActions.Remove(action);
    }
    public override void OnRequestFinished(UnityWebRequest webRequest, long Code)
    {
        base.OnRequestFinished(webRequest, Code);
    } 
    public override void ParseResponse(UnityWebRequest webRequest)
    {
        LoginResponse loginResponse=new LoginResponse();
        loginResponse=JsonConvert.DeserializeObject<LoginResponse>(webRequest.downloadHandler.text);
        loginResponceData.loginResponce=loginResponse;
    }
}
