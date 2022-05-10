using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class C_SendChat : MonoSingleton<C_SendChat>
{
    MessageType messageType;
    public TMP_Text chat_Text;
    public TMP_InputField chat_Input;
    List<string> chatList = new List<string>();
    int countMax = 5;

    #region Register event
    private void OnEnable()
    {
        NetUtility.C_SENDCHAT += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_SENDCHAT -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(SendChatMessage scm)
    {
            NetSendChat nsc = new NetSendChat();
            nsc.ContentBox = scm.ToJson();
            Client.Instance.SendToServer(nsc);
    }
    private void OnEventClient(NetMessage msg)
    {
        SendChatMessage scm = JsonUtility.FromJson<SendChatMessage>((msg as NetSendChat).ContentBox);
            InsertChatToContent(scm.Username + ": <color=\"grey\">" + scm.Chat + "</color>");
    }
    #endregion

    #region Work With UI

    void SetMessageType()
    {
        if (SceneManager.GetActiveScene().name == SceneName.LOBBY)
        {
            messageType = MessageType.TEAMALL;
        }
        else if (SceneManager.GetActiveScene().name == SceneName.ROOM || SceneManager.GetActiveScene().name == SceneName.GAMEXEPHINH)
        {
            messageType = MessageType.TEAMONE;
        }
        else if (SceneManager.GetActiveScene().name == SceneName.GAMESOCCER)
        {
            messageType = MessageType.TEAMTWO;
        }
    }
    private int GetRoomId()
    {
        if (messageType == MessageType.TEAMONE)
        {
            return DataOnClient.Instance.room.RoomId;
        }
        else if (messageType == MessageType.TEAMTWO)
        {
            return DataOnClient.Instance.RoomMatched.RoomId;
        }
        else return -1;
    }
    private void Start()
    {
        SetMessageType();
        chat_Text.text = "";
        chat_Input.onEndEdit.AddListener(delegate { OnEndEditSendChat(chat_Input); });
        InitContent();
    }

    void InitContent()
    {
        chatList = new List<string>();
        for (int i = 0; i < countMax; i++)
        {
            chatList.Add("");
        }
        ConvetListToText();
    }
    void ConvetListToText()
    {
        string content = "";
        foreach (var i in chatList)
        {
            content += "\n" + i;
        }
        chat_Text.text = content;
    }
    void InsertChatToContent(string chat)
    {
        if (chatList.Count == countMax)
        {
            chatList.RemoveAt(0);
        }
        chatList.Add(chat);
        ConvetListToText();
    }

    void OnEndEditSendChat(TMP_InputField chat_Input)
    {
        if (messageType == MessageType.TEAMALL)
        {
            CreateMessageToServer(new SendChatMessage(messageType, DataOnClient.Instance.playerData.DisplayName, chat_Input.text));
        }
        else 
        {
            CreateMessageToServer(new SendChatMessage(messageType, GetRoomId(), DataOnClient.Instance.playerData.DisplayName, chat_Input.text));
        }
        chat_Input.text = "";
    }
    #endregion
}