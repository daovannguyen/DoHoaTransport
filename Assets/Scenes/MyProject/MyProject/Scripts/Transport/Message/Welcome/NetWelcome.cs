using Unity.Networking.Transport;
using UnityEngine;

public class NetWelcome : NetMessage
{
    // cái này máy chủ quản lý;
    public NetWelcome()
    {
        Code = OpCode.WELCOME;
    }
    public NetWelcome(DataStreamReader reader)
    {
        Code = OpCode.WELCOME;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_WELCOME?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_WELCOME?.Invoke(this, cnn);
    }
}
public class WelcomeMessage : BaseMessage<WelcomeMessage>
{
    public int InternalId;
    public WelcomeMessage(int internalId)
    {
        InternalId = internalId;
    }
    public WelcomeMessage()
    {
        InternalId = -1;
    }

    public override string ToJson()
    {
        return base.ToJson();
    }

    public override WelcomeMessage FromJson(string json)
    {
        return base.FromJson(json);
    }
}
