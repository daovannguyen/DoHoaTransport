using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_MovePlayer : MonoBehaviour
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.S_MOVEPLAYER += OnEventServer;
    }

    private void OnDisable()
    {
        NetUtility.S_MOVEPLAYER -= OnEventServer;
    }
    #endregion

    #region Server + message 

    public void CreateMessageToClientCreateSpawn(NetMessage msg, NetworkConnection cnn)
    {

    }
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetMovePlayer nmo = msg as NetMovePlayer;
        MovePlayerMessage mm = JsonUtility.FromJson<MovePlayerMessage>(nmo.ContentBox);
        Server.Instance.BroadCatOnRoom(msg, mm.IdRoom);
    }
    #endregion
}
