using Unity.Networking.Transport;
using UnityEngine;

public class NetSendChat : NetMessage
{
    public NetSendChat()
    {
        Code = OpCode.SENDCHAT;
    }
    public NetSendChat(DataStreamReader reader)
    {
        Code = OpCode.SENDCHAT;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_SENDCHAT?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_SENDCHAT?.Invoke(this, cnn);
    }
}
public class SendChatMessage : BaseMessage<SendChatMessage>
{
    public MessageType messageType;
    public int RoomId;
    public string Username;
    public string Chat;

    // dành cho nhắn toàn hệ thống
    public SendChatMessage(MessageType type, string username, string chat)
    {
        messageType = type;
        Username = username;
        Chat = chat;
    }

    // dành cho nhắn trong room
    public SendChatMessage(MessageType type, int roomId, string username, string chat)
    {
        messageType = type;
        RoomId = roomId;
        Username = username;
        Chat = chat;
    }
}