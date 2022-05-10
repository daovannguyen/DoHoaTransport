using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetBallPosition : NetMessage
{
    public NetBallPosition()  // <-- Making the box
    {
        Code = OpCode.BALLPOSITON;
    }
    public NetBallPosition(DataStreamReader reader) // <-- Receiving the box
    {
        Code = OpCode.BALLPOSITON;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_BALLPOSITION?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_BALLPOSITION?.Invoke(this, cnn);
    }
}

public class BallPositionMessage
{
    public int RoomId;
    public int IdPlayer;
    public Vector3 Postion;
    public Vector3 Rotation;

    public BallPositionMessage(int roomId, int idPlayer, Vector3 postion, Vector3 rotation)
    {
        RoomId = roomId;
        IdPlayer = idPlayer;
        Postion = postion;
        Rotation = rotation;
    }
}
