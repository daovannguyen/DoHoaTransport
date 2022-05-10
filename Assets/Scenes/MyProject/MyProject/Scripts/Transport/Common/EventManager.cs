using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<bool> ReceivePlayerData; // nhận tin nhắn đăng nhập đăng kí
    public static Action<bool> ReceiveRoom; // nhận tin nhắn phòng
                                            // false : lỗi
}
