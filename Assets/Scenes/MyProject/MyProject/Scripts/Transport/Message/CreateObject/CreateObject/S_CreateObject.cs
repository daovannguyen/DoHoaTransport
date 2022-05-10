using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_CreateObject : MonoBehaviour
{

    NetworkConnection Cnn;
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_CREATEOBJECT += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_CREATEOBJECT -= OnEventServer;
    }
    #endregion
    #region Server + message
    public void CreateMessageToClient(NetMessage msg, NetworkConnection cnn)
    {
        NetCreateObject ncp = msg as NetCreateObject;
        CreateObjectMessage oi = JsonUtility.FromJson<CreateObjectMessage>(ncp.ContentBox);
        DataOnServer.Instance.SpawnCount++;
        oi.Id = DataOnServer.Instance.SpawnCount;
        ncp.ContentBox = JsonUtility.ToJson(oi);
        Server.Instance.BroadCatOnRoom(ncp, oi.IdRoom);
    }
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        CreateObjectMessage oi = JsonUtility.FromJson<CreateObjectMessage>((msg as NetCreateObject).ContentBox);
        CreateMessageToClient(msg, cnn);
    }
    #endregion
}
