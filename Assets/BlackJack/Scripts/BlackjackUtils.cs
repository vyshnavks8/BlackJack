using System;
using System.Collections;
using System.IO;
using System.Net.Mail;
using UnityEngine;

public static class BlackjackUtils
{
    public static bool IsValidEmail(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    public static bool IsValidMobile(string mobile)
    {
        return mobile.Length is >= 10 and <= 13;
    }
    public static Sprite GetSprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }
    public static bool IsInputEmpty(string input, string message)
    {
        if (!string.IsNullOrEmpty(input)) return false;
        NetworkPopUp.ShowPopUp("Invalid Input", $"{message} is Required.");
        return true;
    }

    public static void ShowEmpty(string message)
    {
        NetworkPopUp.ShowPopUp("Invalid Input", $"{message} is Required.");
    }

    public static void ShareOnlineGameCode()
    {
        new NativeShare().SetTitle("Join Black jack private game").SetText("Pot BlackJack \n " +
                                                                           AppData.username +
                                                                           " is inviting to play private game \n \n" +
                                                                           "JOIN GAME CODE : " + AppData.onlineGameCode)
            .Share();
    }

    public static IEnumerator GetImageFromFile(Action<Texture2D, string> callback)
    {
        if (NativeGallery.IsMediaPickerBusy()) yield break;
        yield return null;
        NativeGallery.GetImageFromGallery(path =>
        {
            if (path == null) return;
            var texture = NativeGallery.LoadImageAtPath(path, -1,false);
            if (texture != null)
            {
                callback?.Invoke(texture, path);
            }
        }, "Select image");
    }

    public static void ShowDevelopmentPopup()
    {
        var popContent = new PopContent(
            " AI Mode Only in This Build",
            "Private and Public game modes are currently disabled in this build. To test gameplay, please use \"Vs Computer\" ");
        PopUpController.ShowPopUp(popContent, new ButtonContent("Close", PopUpController.ClosePopUp));
    }

    public static WWWForm GetFormImage(Texture2D texture,string key)
    {
        var data = texture.EncodeToPNG();
        var form = new WWWForm();
        form.AddBinaryData(key, data);
        return form;
    }
}