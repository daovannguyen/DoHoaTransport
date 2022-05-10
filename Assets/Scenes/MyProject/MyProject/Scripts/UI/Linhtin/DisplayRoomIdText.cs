using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayRoomIdText : MonoBehaviour
{
    TMP_Text roomName_Text;
    RoomInstance room;
    // Start is called before the first frame update
    void Start()
    {
        roomName_Text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (room != DataOnClient.Instance.room)
        {
            room = DataOnClient.Instance.room;
            roomName_Text.text = "Mã phòng: " + room.RoomId.ToString() + "\n"
             + "Số lượng: " + room.Players.Count.ToString();
        }
    }

    #region Nghe sự kiện
    private void OnEnable()
    {
        EventManager.ReceiveRoom += OnEventReceive;
    }

    private void OnEventReceive(bool obj)
    {
        if (obj)
        {
            roomName_Text.text = "Ma phong: " + room.RoomId.ToString() + "\n"
                + "So luong: " + room.Players.Count.ToString();
        }
    }

    private void OnDisable()
    {
        EventManager.ReceiveRoom -= OnEventReceive;
    }
    #endregion
}
