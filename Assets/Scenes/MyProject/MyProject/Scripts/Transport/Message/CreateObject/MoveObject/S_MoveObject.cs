using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_MoveObject : MonoBehaviour
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_MOVEOBJECT += OnEventServer;
    }

    private void OnDisable()
    {
        NetUtility.S_MOVEOBJECT -= OnEventServer;
    }
    #endregion

    #region Server + message

    public void CreateMessageToClientCreateSpawn(NetMessage msg, NetworkConnection cnn)
    {

    }
    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetMoveObject nmo = msg as NetMoveObject;
        MoveObjectMessage mm = JsonUtility.FromJson<MoveObjectMessage>(nmo.ContentBox); 
        Server.Instance.BroadCatOnRoom(msg, mm.IdRoom);
    }
    #endregion
}
