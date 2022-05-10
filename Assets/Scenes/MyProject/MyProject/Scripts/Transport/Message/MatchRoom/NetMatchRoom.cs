using System;
using Unity.Networking.Transport;
using UnityEngine;

public class NetMatchRoom : NetMessage
{
    public NetMatchRoom()
    {
        Code = OpCode.MATCHROOM;
    }
    public NetMatchRoom(DataStreamReader reader)
    {
        Code = OpCode.MATCHROOM;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_MATCHROOM?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_MATCHROOM?.Invoke(this, cnn);
    }
}
[Serializable]
public class MatchRoomMessage
{
    public RoomInstance Room;
    public MatchRoomMessage(RoomInstance room)
    {
        Room = room;
    }
}