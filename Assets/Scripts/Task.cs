using TMPro;
using UnityEngine;
public class Task : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Task_ID;
    [SerializeField] private TextMeshProUGUI Skill_Name;
    [SerializeField] private TextMeshProUGUI Status;
    [SerializeField] private TextMeshProUGUI Applicaiton;

    public void SetTask(SkillData task)
    {
        Task_ID.text = task.task_id.ToString();
        Skill_Name.text = task.skill_name;
        Status.text = task.status.ToString();
        Applicaiton.text = task.application;
    }
    public void OnClick()
    {
        Debug.Log("Task Clicked "+Task_ID.text);
    }
}
