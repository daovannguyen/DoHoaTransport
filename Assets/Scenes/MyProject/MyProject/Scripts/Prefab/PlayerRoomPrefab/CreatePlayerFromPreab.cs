using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerFromPreab : MonoBehaviour
{
    public GameObject playerRoomPrefab;
    public GameObject contentScroll;

    public void Update()
    {
        var newRoom = DataOnClient.Instance.newRoom;
        var oldRoom = DataOnClient.Instance.room;
        foreach (var i in newRoom.Players)
        {
            if (!RoomInstance.PlayerExistInRoom(i, oldRoom))
            {
                GameObject a = Instantiate(playerRoomPrefab, contentScroll.transform.position, contentScroll.transform.rotation);
                a.transform.parent = contentScroll.transform;
                a.GetComponent<PlayerRoomPrefab>().DisplayName = i.DisplayName;
                a.GetComponent<PlayerRoomPrefab>().Avatar = i.Avatar;
            }
        }
        DataOnClient.Instance.room = newRoom;
    }

}
