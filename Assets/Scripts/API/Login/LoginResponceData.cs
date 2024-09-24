using UnityEngine;


[CreateAssetMenu(fileName = "Responces", menuName = "Responces/LoginResponce", order = 1)]

public class LoginResponceData: ScriptableObject
{
    public LoginResponse loginResponce;
}
[System.Serializable]
public class LoginResponse
{
    public string status;
    public string message;
    public string device;
    public string token;
}
