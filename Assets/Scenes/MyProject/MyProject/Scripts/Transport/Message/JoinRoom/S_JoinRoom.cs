using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_JoinRoom : MonoBehaviour
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.S_JOINROOM += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_JOINROOM -= OnEventServer;
    }
    #endregion

    #region Create Reveice 

    public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        // comment lại vì 1 người có thể trong nhiều nhóm
        //if (RoomInstance.PlayerExistRoom(cnn.InternalId, DataOnServer.Instance.rooms))
        //{
        //    return;
        //}
        NetJoinRoom jr = msg as NetJoinRoom;
        RoomInstance jrm = JsonUtility.FromJson<RoomInstance>(jr.ContentBox);
        //add thành viên khi room có sẵn
        if (jrm.RoomId != -1)
        {
            int indexRoom = RoomInstance.FindIndexRoomByIdRoom(jrm.RoomId, DataOnServer.Instance.rooms);
            // client join, không có room, server trả về room trống
            if (indexRoom == -1)
            {
                NetJoinRoom njr = new NetJoinRoom();
                njr.ContentBox = JsonUtility.ToJson(njr);
                Server.Instance.SendToClient(cnn, njr);
            }
            // thêm thành viên mới và gửi kết quả về toàn đội
            else
            {
                DataOnServer.Instance.rooms[indexRoom].AddPlayer(jrm.Players[0]);
                jrm = DataOnServer.Instance.rooms[indexRoom];
                Debug.Log(indexRoom);

                NetJoinRoom njr = new NetJoinRoom();
                njr.ContentBox = JsonUtility.ToJson(jrm);
                Server.Instance.BroadCatOnRoom(njr, jrm.RoomId);
                //CreateMessageToClient(cnn, JsonUtility.ToJson(jrm));
            }
        }
        // trường hợp phải tạo room
        else
        {
            int idRoomRandom = RoomInstance.GetNewRoomId(DataOnServer.Instance.rooms);
            RoomInstance room = new RoomInstance(idRoomRandom, jrm.Players[0]);
            
            DataOnServer.Instance.rooms.Add(room);
            NetJoinRoom njr = new NetJoinRoom();
            njr.ContentBox = JsonUtility.ToJson(room);
            Server.Instance.SendToClient(cnn, njr);
        }
    }
    #endregion
}
