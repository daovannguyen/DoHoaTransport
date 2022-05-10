using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData
{
    public string Method; // get hoặc set
    public string Username;
    public string Password;
    public string DisplayName;
    public string Khoa;
    public string QueQuan;
    public string DienThoai;
    public string Avatar;
    public PlayerData(string method, string username, string password)
    {
        Method = method;
        Username = username;
        Password = password;
        DisplayName = "";
        Khoa = "";
        QueQuan = "";
        DienThoai = "";
        Avatar = "";
    }
    public PlayerData ToGet()
    {
        Method = "Get";
        return this;
    }
    public PlayerData ToSet()
    {
        Method = "Set";
        return this;
    }
    public PlayerData ToUpdate()
    {
        Method = "Update";
        return this;
    }
    public PlayerData ToSendOtherPlayer()
    {
        PlayerData a = this;
        a.Username = "";
        a.Password = "";
        return a;
    }
}
public class GetPlayerData : MonoSingleton<GetPlayerData>
{
    public void SendToASP(string method, string username, string password)
    {
        PlayerData pd = new PlayerData(method, username, password);
        StartCoroutine(APICall.JsonPost(PathASP.PLAYERDATA, JsonUtility.ToJson(pd), LoginSuccessly, LoginError));
    }

    private void LoginError(UnityWebRequest arg1, string arg2)
    {
        EventManager.ReceivePlayerData?.Invoke(false);
    }

    private void LoginSuccessly(UnityWebRequest arg1, string json)
    {
        PlayerData pd = JsonUtility.FromJson<PlayerData>(json);
        DataOnClient.Instance.playerData = pd;
        DataOnClient.Instance.playerDataUri = pd;
        if (pd.Method == "Error")
        {
            EventManager.ReceivePlayerData?.Invoke(false);
        }
        else
        {
            EventManager.ReceivePlayerData?.Invoke(true);
        }
    }
}
