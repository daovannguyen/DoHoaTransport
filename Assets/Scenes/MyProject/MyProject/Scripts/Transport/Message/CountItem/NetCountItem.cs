using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetCountItem : NetMessage
{
    // cái này máy chủ quản lý;
    public NetCountItem()
    {
        Code = OpCode.COUNTITEM;
    }
    public NetCountItem(DataStreamReader reader)
    {
        Code = OpCode.COUNTITEM;
        Deserialize(reader);
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_COUNTITEM?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_COUNTITEM?.Invoke(this, cnn);
    }
}

public class CountItemMessage
{
    public int IdRoom;
    public int IndexPrefab;
    public int CountChange;

    public CountItemMessage(int idRoom, int indexPrefab, int countChange)
    {
        IdRoom = idRoom;
        IndexPrefab = indexPrefab;
        CountChange = countChange;
    }
    public CountItemMessage()
    {
        IdRoom = -1;
        IndexPrefab = -1;
        CountChange = -1;
    }
}

