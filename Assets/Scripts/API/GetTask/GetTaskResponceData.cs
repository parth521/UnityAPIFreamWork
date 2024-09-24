using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Responces", menuName = "Responces/GetTaskResponceData", order = 1)]
public class GetTaskResponceData : ScriptableObject
{
    public SkillsContainer tasksResponse;
}
[System.Serializable]
public class SkillData
{
    public int task_id;
    public string skill_name;
    public bool status;
    public string application;
    public string starts_at;
    public string ends_at;
}

[System.Serializable]
public class SkillsContainer
{
    public Dictionary<string, SkillData> tasks=new Dictionary<string, SkillData>();
    public string last_opened_task;
}