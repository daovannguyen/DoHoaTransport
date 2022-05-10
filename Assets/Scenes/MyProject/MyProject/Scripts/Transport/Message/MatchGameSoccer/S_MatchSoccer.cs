using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_MatchSoccer : MonoSingleton<S_MatchSoccer>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_MATCHSOCCER += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_MATCHSOCCER -= OnEventServer;
    }
    #endregion

    #region Create Reveice 
    private void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetMatchSoccer wm = msg as NetMatchSoccer;
        MatchSoccerMessage mcm = JsonUtility.FromJson<MatchSoccerMessage>(wm.ContentBox);
        Server.Instance.BroadCatOnRoom(msg, mcm.RoomId);
    }
    #endregion
}
