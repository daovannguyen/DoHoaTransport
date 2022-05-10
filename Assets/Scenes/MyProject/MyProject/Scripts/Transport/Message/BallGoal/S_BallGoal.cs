using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_BallGoal : MonoBehaviour
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.S_BALLGOAL += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_BALLGOAL -= OnEventServer;
    }
    #endregion

    #region Create Reveice 

    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        NetBallGoal jr = msg as NetBallGoal;
        BallGoalMessage bpm = JsonUtility.FromJson<BallGoalMessage>(jr.ContentBox);
        Server.Instance.BroadCatOnRoom(msg, bpm.RoomId);
    }
    #endregion
}
