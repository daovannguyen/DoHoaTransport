using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetMovePlayer : NetMessage
{
    // cái này máy chủ quản lý;
    public NetMovePlayer()
    {
        Code = OpCode.MOVEPLAYER;
    }
    public NetMovePlayer(DataStreamReader reader)
    {
        Code = OpCode.MOVEPLAYER;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_MOVEPLAYER?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_MOVEPLAYER?.Invoke(this, cnn);
    }

}

public class MovePlayerMessage
{ 
    public int IdRoom;
    public int Id;
    public Vector3 Positon;
    public Vector3 Rotation;

    public MovePlayerMessage(int idRoom, int id, Vector3 positon, Vector3 rotation)
    {
        IdRoom = idRoom;
        Id = id;
        Positon = positon;
        Rotation = rotation;
    }
}
