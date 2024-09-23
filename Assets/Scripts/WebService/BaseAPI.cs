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
    public const string BASE_URL = "https://habar.kenda-ai.com";
    public virtual void PostRequest(string data)
    {
        WebServiceManager.Instance.StartCoroutine(PostService(data));
    }
    public virtual void GetRequest(string key, string value)
    {
        WebServiceManager.Instance.StartCoroutine(GetService());
    }
    private IEnumerator GetService()
    {
        string uri = BASE_URL + endPointName;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                OnRequestFinished(webRequest.downloadHandler.text, webRequest.responseCode);
            }
        }
    }
    private IEnumerator PostService(string jsonData)
    {
        string uri = BASE_URL + endPointName;
        using (UnityWebRequest webRequest = new UnityWebRequest(uri, "POST"))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                OnRequestFinished(webRequest.downloadHandler.text, webRequest.responseCode);
            }
        }
    }
    public virtual void OnRequestFinished(string response, long Code)
    {
        switch (Code)
        {
            case 200:
                break;
            case 403:
                break;
            case 400:
                break;
            case 401:
                break;
            case 404:
                break;
            case 423:
                break;
        }
    }
}
