using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetBallGoal : NetMessage
{
    public NetBallGoal()  // <-- Making the box
    {
        Code = OpCode.BALLGOAL;
    }
    public NetBallGoal(DataStreamReader reader) // <-- Receiving the box
    {
        Code = OpCode.BALLGOAL;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_BALLGOAL?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_BALLGOAL?.Invoke(this, cnn);
    }
}

public class BallGoalMessage
{
    public int RoomId;
    public int PlayerIdControl;
    public int ScoreHost;
    public int ScoreNoHost;

    public BallGoalMessage(int roomId, int playerIdControl, int scoreHost, int scoreNoHost)
    {
        RoomId = roomId;
        PlayerIdControl = playerIdControl;
        ScoreHost = scoreHost;
        ScoreNoHost = scoreNoHost;
    }
}
