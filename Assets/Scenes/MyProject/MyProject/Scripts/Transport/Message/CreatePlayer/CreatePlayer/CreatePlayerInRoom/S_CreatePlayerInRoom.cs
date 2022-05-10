using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_CreatePlayerInRoom : MonoBehaviour
{

    NetworkConnection Cnn;
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_CREATEPLAYERINROOM += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_CREATEPLAYERINROOM -= OnEventServer;
    }
    #endregion
    #region Server + message
    public void CreateMessageToClient(NetMessage msg, NetworkConnection cnn)
    {
        NetCreatePlayerInRoom ncp = msg as NetCreatePlayerInRoom;
        CreatePlayerInRoomMessage com = JsonUtility.FromJson<CreatePlayerInRoomMessage>(ncp.ContentBox);
        com.Id = cnn.InternalId;
        ncp.ContentBox = JsonUtility.ToJson(com);
        Server.Instance.BroadCatOnRoom(ncp, com.IdRoom);
    }
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        CreateMessageToClient(msg, cnn);
    }
    #endregion
}
