using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Server : MonoSingleton<Server>
{
    #region property
    public NetworkDriver driver;
    protected NativeList<NetworkConnection> connections;

    private bool isActive = false;
    private const float keepAliveTickRate = 20.0f;
    private float lastKeepAlive;

    public Action connectionDropped;
    #endregion

    // method
    public void Init(ushort port)
    {
        // init the drive
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4; // WHO can connect to us
        endpoint.Port = port;
        if (driver.Bind(endpoint) != 0)
        {
            Debug.Log("There was error binding to port " + endpoint.Port);
            return;
        }
        else
        {
            driver.Listen();
            Debug.Log("Server is created");
        }

        // init the connection list
        connections = new NativeList<NetworkConnection>(100, Allocator.Persistent);
        isActive = true;
        // 4: số lượng tối đa, 
        // Allocator.Persistent: trình phân bổ hoạt động ổn định

    }
    public void Shutdown()
    {
        if (isActive)
        {
            driver.Dispose();
            connections.Dispose();
            isActive = false;
        }
    }
    public void OnDestroy()
    {
        Shutdown();
    }
    public void Update()
    {
        if (!isActive)
            return;

        KeepAlive();

        driver.ScheduleUpdate().Complete();
        CleanupConnections(); // xóa các kết nối bị ngắt khi chưa nhận được kết quả
        AcceptNewConnections();
        UpdateMessagePump();
    }
    private void KeepAlive()
    {
        if (Time.time - lastKeepAlive > keepAliveTickRate)
        {
            lastKeepAlive = Time.time;
            BroadCat(new NetKeepAlive());
        }
    }
    private void CleanupConnections() // không phải là hàm virtual
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }
    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream; // luồng này đọc được khá nhiều loại dữ liệu vì nó đọc từng bit

        for (int i = 0; i < connections.Length; i++)
        {

            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    NetUtility.OnData(stream, connections[i], this);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                }
            }
        }
    }

    // Server specific
    public void BroadCat(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                Debug.Log($"Sending {msg.Code} to: {connections[i].InternalId}");
                SendToClient(connections[i], msg);
            }
        }
    }
    public void BroadCatOnRoom(NetMessage msg, int idRoom)
    {
        int indexRoom = RoomInstance.FindIndexRoomByIdRoom(idRoom, DataOnServer.Instance.rooms);
        RoomInstance room = DataOnServer.Instance.rooms[indexRoom];
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                foreach (var j in room.Players)
                {
                    if (j.Id == connections[i].InternalId)
                    {
                        Debug.Log($"Sending {msg.Code} to: {connections[i].InternalId}");
                        SendToClient(connections[i], msg);
                    } 
                }
            }
        }
    }
    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }
}
