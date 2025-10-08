using System;

public class GetProfileResponse : BaseResponse
{
    public User user;
}



public class User
{
    
    public string _id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string mobileNo { get; set; }
    public string profileImage { get; set; }
    public DateTime createdAt { get; set; }
}