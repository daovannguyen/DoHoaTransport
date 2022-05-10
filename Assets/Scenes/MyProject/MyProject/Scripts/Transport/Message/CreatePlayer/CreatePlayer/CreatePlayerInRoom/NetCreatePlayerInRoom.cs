using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetCreatePlayerInRoom : NetMessage
{
    // cái này máy chủ quản lý;
    public NetCreatePlayerInRoom()
    {
        Code = OpCode.CREATEPLAYERINROOM;
    }
    public NetCreatePlayerInRoom(DataStreamReader reader)
    {
        Code = OpCode.CREATEPLAYERINROOM;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_CREATEPLAYERINROOM?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_CREATEPLAYERINROOM?.Invoke(this, cnn);
    }
}

public class CreatePlayerInRoomMessage
{
    public int IdRoom;
    public int Id;
    public string DisplayName;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public CreatePlayerInRoomMessage(int idRoom, int id, string displayName, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        IdRoom = idRoom;
        Id = id;
        DisplayName = displayName;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}

