using System;

public static class BlackJackApi
{
    public static void GetProfile(Action<bool> callback=null)
    {
        APIHandler.Get<GetProfileResponse>(ApiUrl.Profile, null,(success,response)=> GetProfileCallback(success,response,callback));
    }

    private static void GetProfileCallback(bool success, GetProfileResponse response,Action<bool> callback)
    {
        if (success)
        {
            var userName = response.user.name;
            var email = response.user.email;
            var mobile = response.user.mobileNo;
            AppData.SetUserData(userName, email, mobile);
        }
        callback?.Invoke(success);
    }
}