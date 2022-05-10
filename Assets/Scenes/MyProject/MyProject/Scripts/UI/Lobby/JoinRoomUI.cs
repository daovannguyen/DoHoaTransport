using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class JoinRoomUI : MonoSingleton<JoinRoomUI>
{
    public TMP_InputField roomId_Input;
    public TMP_Text warming_Text;
    public Button create_Btn;
    public Button join_Btn;
    private void Init()
    {
        warming_Text.text = "";
        create_Btn.onClick.AddListener(OnClickCreate);
        join_Btn.onClick.AddListener(OnClickJoin);
    }
    private void Awake()
    {
        Init();
    }
    private void OnClickJoin()
    {
        C_JoinRoom.Instance.CreateMessageToServer(Convert.ToInt32(roomId_Input.text));
    }

    private void OnClickCreate()
    {
        C_JoinRoom.Instance.CreateMessageToServer();
    }

    #region Register event
    private void OnEnable()
    {
        EventManager.ReceiveRoom += OnReceiRoom;
    }
    private void OnDisable()
    {
        EventManager.ReceiveRoom -= OnReceiRoom;
    }

    private void OnReceiRoom(bool obj)
    {
        if (obj)
        {
            SceneManager.LoadScene("Room");
        }
        else
        {
            warming_Text.text = "Khong co phong!";
        }
    }
    #endregion
}
