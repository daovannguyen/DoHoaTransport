using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetCreateObject : NetMessage
{
    // cái này máy chủ quản lý;
    public NetCreateObject()
    {
        Code = OpCode.CREATEOBJECT;
    }
    public NetCreateObject(DataStreamReader reader)
    {
        Code = OpCode.CREATEOBJECT;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_CREATEOBJECT?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_CREATEOBJECT?.Invoke(this, cnn);
    }
}

public class CreateObjectMessage
{
    public int IdRoom;
    public int Id;
    public int IndexPrefab;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public CreateObjectMessage(int idRoom, int id, int indexPrefab, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        IdRoom = idRoom;
        Id = id;
        IndexPrefab = indexPrefab;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
    public CreateObjectMessage()
    {
        IdRoom = -1;
        Id = -1;
        IndexPrefab = -1;
        Position = Vector3.zero;
        Rotation = Vector3.zero; 
        Scale = Vector3.zero; 
    }
}

