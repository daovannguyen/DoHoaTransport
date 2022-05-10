using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class C_MatchRoom : MonoSingleton<C_MatchRoom>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_MATCHROOM += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_MATCHROOM -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    public void CreateMessageToServer(RoomInstance room)
    {
        RoomInstance roomMatch = room.StatusToMatch();
        MatchRoomMessage scm = new MatchRoomMessage(roomMatch);
        NetMatchRoom nsc = new NetMatchRoom();
        nsc.ContentBox = JsonUtility.ToJson(scm);
        Client.Instance.SendToServer(nsc);
    }
    private void OnEventClient(NetMessage msg)
    {
        MatchRoomMessage scm = JsonUtility.FromJson<MatchRoomMessage>((msg as NetMatchRoom).ContentBox);
        DataOnClient.Instance.RoomMatched = scm.Room;
        SceneManager.LoadScene(SceneName.GAMESOCCER);
    }
    #endregion

    #region Work With UI
    #endregion
}