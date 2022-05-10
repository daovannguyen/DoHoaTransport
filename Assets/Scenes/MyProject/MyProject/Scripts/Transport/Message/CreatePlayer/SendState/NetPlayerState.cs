using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetPlayerState : NetMessage
{
    // cái này máy chủ quản lý;
    public NetPlayerState()
    {
        Code = OpCode.PLAYERSTATE;
    }
    public NetPlayerState(DataStreamReader reader)
    {
        Code = OpCode.PLAYERSTATE;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_PLAYERSTATE?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_PLAYERSTATE?.Invoke(this, cnn);
    }

}
public class PlayerStateMessage
{
    public MessageType messageType;
    public int IdRoom;
    public int Id;
    public string Trigger;

    public PlayerStateMessage(string trigger)
    {
        Trigger = trigger;
    }
}
