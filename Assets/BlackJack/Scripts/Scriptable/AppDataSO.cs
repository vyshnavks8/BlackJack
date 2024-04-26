using UnityEngine;

[CreateAssetMenu(menuName = "Create UserData", fileName = "UserData", order = 0)]
public class AppDataSO : ScriptableObject
{
    public string username;
    public string email;
    public string mobile;

    public void SetUserData(string userName,string emailId, string mobileNo)
    {
        username = userName;
        email = emailId;
        mobile = mobileNo;
    }
}