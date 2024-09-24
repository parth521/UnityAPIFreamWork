using System;
using UnityEngine;
public class LevelSelectionScreen : UIElement
{
    [SerializeField]private GetTaskApi getTaskApi;
    [SerializeField] private LoginResponceData loginResponceData;
    [SerializeField] private GetTaskResponceData getTaskResponceData;
    [SerializeField] private Task _task;
    [SerializeField] private Transform taskContainer;
    public override void Show(Action callback = null)
    {
        getTaskApi.AddsuccesAction(OnGettingTaskSuccess);
        base.Show();
        UIManager.Instance.ShowScreen(UIScreen.loadingScreen);
        GetTaskApi();
    }
    public override void Hide(Action callback = null)
    {
        getTaskApi.RemoveSuccessAction(OnGettingTaskSuccess);
        base.Hide();
    }
    private void GetTaskApi()
    {
        getTaskApi.GetRequest(loginResponceData.loginResponce.token);
    }
    private void OnGettingTaskSuccess()
    {
        foreach (Transform child in taskContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (var task in getTaskResponceData.tasksResponse.tasks)
        {
            Task newTask = Instantiate(_task, taskContainer);
            newTask.SetTask(task.Value);
        }
        UIManager.Instance.HideScreen(UIScreen.loadingScreen);
    }
}
