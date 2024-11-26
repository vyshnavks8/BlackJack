using System;
using Newtonsoft.Json;
using RestAPI;
using UnityEngine;
using UnityEngine.Networking;

public class APIHandler : ApiBase
{
    public static void Post<T>(string url, object requestData, Action<bool, T> callBack, bool setAuth = false)
        where T : BaseResponse
    {
        SendWithMethod(url, requestData, UnityWebRequest.kHttpVerbPOST, callBack, setAuth);
    }

    public static void Get<T>(string url, object requestData, Action<bool, T> callBack, bool setAuth = false)
        where T : BaseResponse
    {
        SendWithMethod(url, requestData, UnityWebRequest.kHttpVerbGET, callBack, setAuth);
    }

    public static void Put<T>(string url, object requestData, Action<bool, T> callBack, bool setAuth = false)
        where T : BaseResponse
    {
        SendWithMethod(url, requestData, UnityWebRequest.kHttpVerbPUT, callBack, setAuth);
    }

    public static void Delete<T>(string url, object requestData, Action<bool, T> callBack, bool setAuth = false)
        where T : BaseResponse
    {
        SendWithMethod(url, requestData, UnityWebRequest.kHttpVerbDELETE, callBack, setAuth);
    }

    public static void SendWithMethod<T>(string url, object requestData, string method, Action<bool, T> callBack,
        bool setAuth = false) where T : BaseResponse
    {
        var json = string.Empty;
        if (requestData != null)
        {
            json = JsonConvert.SerializeObject(requestData);
        }

        WebRequestMethod(url, json, method, (_, success, data) => HandleCallback(success, data, callBack, setAuth));
    }


    private static void HandleCallback<T>(bool success, object data, Action<bool, T> callBack, bool setAuth)
        where T : BaseResponse
    {
        if (!success)
        {
            var response = JsonConvert.DeserializeObject<T>(data.ToString());
            callBack?.Invoke(false, response);
        }
        else
        {
            var response = JsonConvert.DeserializeObject<T>(data.ToString());
            if (setAuth)
            {
                SetAuthToken(response.token);
            }

            callBack?.Invoke(response.success, response);
        }
    }
}