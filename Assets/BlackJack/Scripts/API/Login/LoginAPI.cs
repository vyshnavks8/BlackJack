using System;
using Newtonsoft.Json;
using RestAPI;

public class LoginAPI : ApiBase
{
    private const string ID = "login";
    
    public static void Send(LoginData requestData, Action<bool,LoginResponse> callBack)
    {
        var json = JsonConvert.SerializeObject(requestData);
        WebRequest(ApiUrl.User + ID, json, (_, success, data) => HandleCallback(success, data, callBack));
    }

    private static void HandleCallback(bool success, object data, Action<bool,LoginResponse> callBack)
    {
        if (!success)
        {
            callBack?.Invoke(false, null);
        }
        else
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(data.ToString());
            SetAuthToken(response.token);
            callBack?.Invoke(response.success, response);
        }
    }
}