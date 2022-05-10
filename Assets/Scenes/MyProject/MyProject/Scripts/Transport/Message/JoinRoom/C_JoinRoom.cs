using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class C_JoinRoom : MonoSingleton<C_JoinRoom>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_JOINROOM += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_JOINROOM -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        RoomInstance jrm = JsonUtility.FromJson<RoomInstance>((msg as NetJoinRoom).ContentBox);
        if (jrm.RoomId == -1)
        {
            EventManager.ReceiveRoom?.Invoke(false);
        }
        else
        {
            DataOnClient.Instance.newRoom = jrm;
            if (jrm.HostPlayer.Id == DataOnClient.Instance.InternalId)
            {
                DataOnClient.Instance.SetPropertyHost();
            }
            EventManager.ReceiveRoom?.Invoke(true);
        }
    }
    public void CreateMessageToServer(int roomId = -1, RoomType roomType = RoomType.CREATEROOM)
    {
        RoomInstance jrm = new RoomInstance();
        PlayerInRoom playerInRoom = new PlayerInRoom(DataOnClient.Instance.InternalId, DataOnClient.Instance.playerData.DisplayName, DataOnClient.Instance.playerDataUri.Avatar);
        jrm.Players.Add(playerInRoom);
        jrm.Type = roomType;
        jrm.RoomId = roomId;
        NetJoinRoom njr = new NetJoinRoom();
        njr.ContentBox = JsonUtility.ToJson(jrm);
        Client.Instance.SendToServer(njr);
    }

    
    #endregion
}
