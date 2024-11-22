using System;
using Newtonsoft.Json;
using RestAPI;
using UnityEngine;

public class APIHandler : ApiBase
{
    public static void Post<T>(string url,object requestData, Action<bool,T> callBack,bool setAuth=false) where T : BaseResponse
    {
        var json = JsonConvert.SerializeObject(requestData);
        WebRequest(url, json, (_, success, data) => HandleCallback(success, data, callBack,setAuth));
    }
    
    public static void SendWithMethod<T>(string url,object requestData,string method, Action<bool,T> callBack,bool setAuth=false) where T : BaseResponse
    {
        var json = JsonConvert.SerializeObject(requestData);
        WebRequestMethod(url, json, method, (_, success, data) => HandleCallback(success, data, callBack,setAuth));
    }
    
    public static void Get<T>(string url,object requestData, Action<bool,T> callBack,bool setAuth=false) where T : BaseResponse
    {
        var json=string.Empty;
        if (requestData != null)
        {
            json = JsonConvert.SerializeObject(requestData);
        }
        WebRequestGet(url, json, (_, success, data) => HandleCallback(success, data, callBack,setAuth));
    }

    private static void HandleCallback<T>(bool success, object data, Action<bool, T> callBack, bool setAuth)  where T : BaseResponse
    {
        Debug.Log(typeof(T)+": "+ success);
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