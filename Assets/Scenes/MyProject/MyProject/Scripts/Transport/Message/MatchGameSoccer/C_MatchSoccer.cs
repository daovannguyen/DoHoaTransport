using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_MatchSoccer : MonoSingleton<C_MatchSoccer>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_MATCHSOCCER += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_MATCHSOCCER -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        RoomUIManager.Instance.OpenLoading();
    }
    public void CreateMessageToServer(MatchSoccerMessage mcm)
    {
        NetMatchSoccer nmc = new NetMatchSoccer();
        nmc.ContentBox = JsonUtility.ToJson(mcm);
        Client.Instance.SendToServer(nmc);
    }
    #endregion
}
