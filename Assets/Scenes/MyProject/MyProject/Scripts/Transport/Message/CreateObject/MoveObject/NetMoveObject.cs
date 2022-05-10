using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetMoveObject : NetMessage
{
    // cái này máy chủ quản lý;
    public NetMoveObject()
    {
        Code = OpCode.MOVEOBJECT;
    }
    public NetMoveObject(DataStreamReader reader)
    {
        Code = OpCode.MOVEOBJECT;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_MOVEOBJECT?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_MOVEOBJECT?.Invoke(this, cnn);
    }
}

public enum MoveObjectType{
    TRANSLATE,
    ROTATE,
    CAMERA,
    OBJECT
}
public class MoveObjectMessage
{
    public int IdRoom;
    public int Id;
    public MoveObjectType Type;
    public Vector3 Target;

    public MoveObjectMessage(int idRoom, int id, MoveObjectType type, Vector3 target)
    {
        IdRoom = idRoom;
        Id = id;
        Type = type;
        Target = target;
    }
    public MoveObjectMessage()
    {
        IdRoom = -1;
        Id = -1;
        Type = MoveObjectType.TRANSLATE;
        Target = Vector3.zero;
    }
}

