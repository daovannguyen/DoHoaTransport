using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIM : MonoSingleton<LobbyUIM>
{
    public void SetUpLobby()
    {
        LobbyUIManager.Instance.LChat_Text.text = "";
    }
    private void OnEnable()
    {
        EventManager.ReceivePlayerData += OnReceiveData;
    }
    private void OnDisable()
    {
        EventManager.ReceivePlayerData -= OnReceiveData;
    }

    private void OnReceiveData(bool obj)
    {
        if (obj)
        {
            string base64 = DataOnClient.Instance.playerData.Avatar;
        }
    }
}
