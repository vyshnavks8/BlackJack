using System;
using Newtonsoft.Json;
using RestAPI;

public class SignUpAPI : ApiBase
{
    private const string ID = "signup";
    
    public static void Send(SignUpData requestData, Action<bool,SignUpResponse> callBack)
    {
        var json = JsonConvert.SerializeObject(requestData);
        WebRequest(ApiUrl.User + ID, json, (_, success, data) => HandleCallback(success, data, callBack));
    }

    private static void HandleCallback(bool success, object data, Action<bool,SignUpResponse> callBack)
    {
        if (!success)
        {
            callBack?.Invoke(false, null);
        }
        else
        {
            var response = JsonConvert.DeserializeObject<SignUpResponse>(data.ToString());
            callBack?.Invoke(response.success, response);
        }
    }
}

public class APIHandler : ApiBase
{
    // public static void Send(SignUpData requestData, Action<bool,SignUpResponse> callBack)
    // {
    //     var json = JsonConvert.SerializeObject(requestData);
    //     WebRequest(ApiUrl.User + ID, json, (_, success, data) => HandleCallback(success, data, callBack));
    // }
}