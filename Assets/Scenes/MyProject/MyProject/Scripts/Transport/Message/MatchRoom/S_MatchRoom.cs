using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_MatchRoom : MonoSingleton<S_MatchRoom>
{
    NetworkConnection Cnn;
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_MATCHROOM += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_MATCHROOM -= OnEventServer;
    }
    #endregion

    #region Server
    private void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        int indexRoomMatchingExists = RoomInstance.FindIndexRoomMatching(DataOnServer.Instance.rooms);

        NetMatchRoom jr = msg as NetMatchRoom;
        MatchRoomMessage jrm = JsonUtility.FromJson<MatchRoomMessage>(jr.ContentBox);
        int indexRoomCurren = RoomInstance.FindIndexRoomByIdRoom(jrm.Room.RoomId, DataOnServer.Instance.rooms);

        // Không có sẵn room đang match
        if (indexRoomMatchingExists == -1)
        {
            DataOnServer.Instance.rooms[indexRoomCurren].Status = RoomStatus.MATCH;
        }
        else
        {
            RoomInstance newRoom = new RoomInstance();
            
            newRoom.RoomId = RoomInstance.GetNewRoomId(DataOnServer.Instance.rooms);
            newRoom.HostPlayer = DataOnServer.Instance.rooms[indexRoomMatchingExists].HostPlayer;
            newRoom.Type = RoomType.CREATEROOM;
            newRoom.Status = RoomStatus.NORMAL;

            foreach (var i in DataOnServer.Instance.rooms[indexRoomMatchingExists].Players)
            {
                newRoom.Players.Add(i);
            }
            foreach (var i in DataOnServer.Instance.rooms[indexRoomCurren].Players)
            {
                newRoom.Players.Add(i);
            }

            // cập nhật lại room đang match
            DataOnServer.Instance.rooms[indexRoomMatchingExists].Status = RoomStatus.NORMAL;
            DataOnServer.Instance.rooms.Add(newRoom);

            NetMatchRoom nmr = new NetMatchRoom();
            nmr.ContentBox = JsonUtility.ToJson(new MatchRoomMessage(newRoom));
            
            Server.Instance.BroadCatOnRoom(nmr, newRoom.RoomId);
        }
    }
    // send toàn bộ
    public void CreateMessageToClient(NetMessage msg)
    {
        Server.Instance.BroadCat(msg);
    }
    #endregion
}