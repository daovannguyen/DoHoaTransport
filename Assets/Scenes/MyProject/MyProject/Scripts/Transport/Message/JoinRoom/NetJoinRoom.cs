using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetJoinRoom : NetMessage
{
    public NetJoinRoom()  // <-- Making the box
    {
        Code = OpCode.JOINROOM;
    }
    public NetJoinRoom(DataStreamReader reader) // <-- Receiving the box
    {
        Code = OpCode.JOINROOM;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_JOINROOM?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_JOINROOM?.Invoke(this, cnn);
    }
}

public class JoinRoomMessage: BaseMessage<JoinRoomMessage>
{

}
