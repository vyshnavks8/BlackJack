using System;
using UnityEngine;

public static class BlackJackApi
{
    public static void GetProfile(Action<bool> callback=null)
    {
        LoadingController.ShowLoading();
        APIHandler.Get<GetProfileResponse>(ApiUrl.Profile, null,(success,response)=> GetProfileCallback(success,response,callback));
    }

    private static void GetProfileCallback(bool success, GetProfileResponse response,Action<bool> callback)
    {
        LoadingController.HideLoading();
        if (success)
        {
            var userName = response.user.name;
            var email = response.user.email;
            var mobile = response.user.mobileNo;
            var iconUrl = response.user.profileImage;
            AppData.SetUserData(userName, email, mobile,iconUrl);
        }
        callback?.Invoke(success);
    }

    public static void GetProfileIcon(string url,Action callback=null)
    {
        if(string.IsNullOrEmpty(url))  return;
        LoadingController.ShowLoading();
        APIHandler.GetImage(url,(s,t)=>GetImageCallback(s,t,callback));
    }
    
    private static void GetImageCallback(bool success, Texture2D texture,Action callback)
    {
        LoadingController.HideLoading();
        if (success)
        {
            var sprite = BlackjackUtils.GetSprite(texture);
            AppData.SetUserIcon(sprite);
        }
        callback?.Invoke();
    }
}