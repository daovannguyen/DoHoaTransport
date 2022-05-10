using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_PlayerState : MonoBehaviour
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.S_PLAYERSTATE += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_PLAYERSTATE -= OnEventServer;
    }
    #endregion

    #region Server
    private void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetPlayerState nps = msg as NetPlayerState;
        PlayerStateMessage ps = JsonUtility.FromJson<PlayerStateMessage>(nps.ContentBox);
        Server.Instance.BroadCatOnRoom(msg, ps.IdRoom);
    }
    // send toàn bộ
    public void CreateMessageToClient(NetMessage msg)
    {
    }
    #endregion
}
