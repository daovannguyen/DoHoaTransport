using Unity.Networking.Transport;
using UnityEngine;

public class NetMatchSoccer : NetMessage
{
    // cái này máy chủ quản lý;
    public NetMatchSoccer()
    {
        Code = OpCode.MATCHSOCCER;
    }
    public NetMatchSoccer(DataStreamReader reader)
    {
        Code = OpCode.MATCHSOCCER;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_MATCHSOCCER?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_MATCHSOCCER?.Invoke(this, cnn);
    }
}
public class MatchSoccerMessage
{
    public int RoomId;

    public MatchSoccerMessage(int roomId)
    {
        RoomId = roomId;
    }
}
