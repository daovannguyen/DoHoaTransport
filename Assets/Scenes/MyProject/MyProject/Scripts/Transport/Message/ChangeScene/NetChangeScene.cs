using Unity.Networking.Transport;
using UnityEngine;

public class NetChangeScene : NetMessage
{
    // cái này máy chủ quản lý;
    public NetChangeScene()
    {
        Code = OpCode.CHANGESCENE;
    }
    public NetChangeScene(DataStreamReader reader)
    {
        Code = OpCode.CHANGESCENE;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_CHANGESCENE?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_CHANGESCENE?.Invoke(this, cnn);
    }
}
public class ChangeSceneMessage : BaseMessage<ChangeSceneMessage>
{
    public int IdRoom;
    public string Name;

    public ChangeSceneMessage(int idRoom, string name)
    {
        IdRoom = idRoom;
        Name = name;
    }
    public ChangeSceneMessage()
    {
        IdRoom = -1;
        Name = "";
    }

    public override ChangeSceneMessage FromJson(string json)
    {
        return base.FromJson(json);
    }

    public override string ToJson()
    {
        return base.ToJson();
    }
}
