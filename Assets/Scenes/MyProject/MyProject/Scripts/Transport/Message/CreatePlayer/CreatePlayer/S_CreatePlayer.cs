using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_CreatePlayer : MonoBehaviour
{

    NetworkConnection Cnn;
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_CREATEPLAYER += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_CREATEPLAYER -= OnEventServer;
    }
    #endregion
    #region Server + message
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetCreatePlayer ncp = msg as NetCreatePlayer;
        CreatePlayerMessage com = JsonUtility.FromJson<CreatePlayerMessage>(ncp.ContentBox);
            com.Id = cnn.InternalId;
            ncp.ContentBox = JsonUtility.ToJson(com);
            Server.Instance.BroadCatOnRoom(ncp, com.IdRoom);
    }

    #endregion
}
