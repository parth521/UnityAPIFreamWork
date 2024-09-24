using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class BaseAPI : ScriptableObject
{
    public List<Action> OnSuccessActions;
    public List<Action> OnFailedActions;
    public List<Action> OnUnAuthorizedActions;
    public string endPointName;
    public const string BASE_URL = "https://habar.kenda-ai.com/api/playground/v1";
    public virtual void PostRequest(WWWForm data)
    {
        WebServiceManager.Instance.StartCoroutine(PostService(data));
    }
    public virtual void GetRequest(string token)
    {
        WebServiceManager.Instance.StartCoroutine(GetService(token));
    }
    private IEnumerator GetService(string token)
    {
        string uri = BASE_URL + endPointName;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + token);
            yield return webRequest.SendWebRequest();
            OnRequestFinished(webRequest, webRequest.responseCode);

        }
    }
    private IEnumerator PostService(WWWForm form)
    {
        string uri = BASE_URL + endPointName;
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            yield return webRequest.SendWebRequest();
            OnRequestFinished(webRequest, webRequest.responseCode);
        }
    }
    public virtual void OnRequestFinished(UnityWebRequest webRequest, long Code)
    {
        switch (Code)
        {
            case 200:
                ParseResponse(webRequest);
                OnSuccess();
                break;
            case 403:
                Debug.LogError("Forbidden: Access is denied.");
                OnFailed();
                break;
            case 400:
                Debug.LogError("Bad Request: " + webRequest.error);
                OnFailed();
                break;
            case 401:
                Debug.LogError("Unauthorized: Authentication required.");
                OnUnAuthorized();
                break;
            case 404:
                Debug.LogError("Not Found: " + webRequest.error);
                OnFailed();
                break;
            case 422:
                Debug.LogError("Not Found: " + webRequest.error);
                OnFailed();
                break;
            default:
                Debug.LogError("Error: " + webRequest.error);
                OnFailed();
                break;
        }
    }
    public virtual void OnSuccess()
    {
        ExecuteActions(OnSuccessActions);
    }
    public virtual void OnFailed()
    {
        ExecuteActions(OnFailedActions);
    }
    public virtual void OnUnAuthorized()
    {
        ExecuteActions(OnUnAuthorizedActions);
    }
    public virtual void ParseResponse(UnityWebRequest webRequest)
    {
        Debug.Log(webRequest.downloadHandler.text);
    }
    public void ExecuteActions(List<System.Action> actions)
    {
        for (int indexOfAction = 0; indexOfAction < actions.Count; indexOfAction++)
        {
            actions[indexOfAction]();
        }
    }
}
