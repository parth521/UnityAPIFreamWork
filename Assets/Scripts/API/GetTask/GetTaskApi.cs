using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "API", menuName = "API/GetTaskApi", order = 1)]
public class GetTaskApi : BaseAPI
{
    [SerializeField] private GetTaskResponceData getTaskResponceData;
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
        SkillsContainer skillsContainer=new SkillsContainer();
        var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(webRequest.downloadHandler.text);
        if (jsonDict != null && jsonDict.ContainsKey("data") && jsonDict.ContainsKey("last_opened_task"))
        {
            var dataDict = JsonConvert.DeserializeObject<Dictionary<string, SkillData>>(jsonDict["data"].ToString());
            skillsContainer.last_opened_task = jsonDict["last_opened_task"].ToString();

            foreach (var skill in dataDict)
            {
                skillsContainer.tasks.Add(skill.Key,skill.Value);
            }
        }
        else
        {
            Debug.LogError("Failed to parse JSON or no data found.");
        }
        getTaskResponceData.tasksResponse = skillsContainer;
    }

}
