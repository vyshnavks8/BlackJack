public static class BlackJackApi
{
    public static void GetProfile()
    {
        APIHandler.Get<GetProfileResponse>(ApiUrl.Profile, null, GetProfileCallback);
    }

    private static void GetProfileCallback(bool success, GetProfileResponse response)
    {
        if (success)
        {
            var userName = response.user.name;
            var email = response.user.email;
            var mobile = response.user.mobileNo;
            AppData.SetUserData(userName, email, mobile);
        }
    }
}