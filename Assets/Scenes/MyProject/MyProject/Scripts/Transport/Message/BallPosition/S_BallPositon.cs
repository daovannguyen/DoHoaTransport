using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_BallPositon : MonoBehaviour
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.S_BALLPOSITION += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_BALLPOSITION -= OnEventServer;
    }
    #endregion

    #region Create Reveice 

    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetBallPosition jr = msg as NetBallPosition;
        BallPositionMessage bpm = JsonUtility.FromJson<BallPositionMessage>(jr.ContentBox);
        Server.Instance.BroadCatOnRoom(msg, bpm.RoomId);
    }
    #endregion
}
