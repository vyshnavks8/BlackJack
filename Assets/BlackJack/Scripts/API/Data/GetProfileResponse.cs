using System;

public class GetProfileResponse : BaseResponse
{
    public User user;
}

public class User
{
    public string _id;
    public string name;
    public string email;
    public string mobileNo;
    public DateTime createdAt;
}