using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class APICall
{
    [Header("Api Call")]
    public static string jsonStringRequest = "";
    public static string jsonStringResponse ="";

    public static string Domain()
    {
        return "daovannguyen8606-001-site1.ftempurl.com/";
        //return "localhost:44334/";
    }
    public static IEnumerator JsonPost(string uri, string jsonString = "{}", Action<UnityWebRequest, string> callbackSuccesslyPHP = null, Action<UnityWebRequest, string> callbackErrorPHP = null)
    {
        yield return RequestJson("POST", uri, jsonString, callbackSuccesslyPHP, callbackErrorPHP);
    }


    public static IEnumerator RequestJson(string method, string uri, string jsonString, Action<UnityWebRequest, string> callbackSuccess = null, Action<UnityWebRequest, string> callbackError = null)
    {
        jsonStringRequest = jsonString;

        string url = Url(uri);
        WWWForm form = new WWWForm();
        form.AddField("data", jsonStringRequest);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            jsonStringResponse = www.downloadHandler.text.Replace("&quot;", "\"");
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                callbackError(www, jsonStringResponse);
            }
            else
            {
                callbackSuccess(www, jsonStringResponse);
            }
        }
    }


    public static string Url(string uri)
    {
        return Protocol() + Domain() + uri;
    }
    public static string Protocol()
    {
        return "http://";
    }
}
