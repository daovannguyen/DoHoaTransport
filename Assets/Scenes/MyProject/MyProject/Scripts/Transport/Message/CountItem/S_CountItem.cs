using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_CountItem : MonoBehaviour
{
    NetworkConnection Cnn;
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_COUNTITEM += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_COUNTITEM -= OnEventServer;
    }
    #endregion
    #region Server + message
    public void CreateMessageToClient(NetMessage msg, NetworkConnection cnn)
    {
        NetCountItem ncp = msg as NetCountItem;
        CountItemMessage oi = JsonUtility.FromJson<CountItemMessage>(ncp.ContentBox);
        Server.Instance.BroadCatOnRoom(ncp, oi.IdRoom);
    }
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        CreateMessageToClient(msg, cnn);
    }
    #endregion
}
