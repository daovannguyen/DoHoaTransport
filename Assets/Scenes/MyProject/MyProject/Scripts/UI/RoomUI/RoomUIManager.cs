using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomUIManager : MonoSingleton<RoomUIManager>
{
    public Button BongDaButton;

    [Header("Screens")]
    public GameObject RoomUI;
    public GameObject LoadingUI;
    public GameObject[] Screens;
    private enum ScreenSeleced
    {
        Room,
        Loading,
    }


    private void AddScreenToArray()
    {
        Screens = new GameObject[10];
        Screens[(int)ScreenSeleced.Room] = RoomUI;
        Screens[(int)ScreenSeleced.Loading] = LoadingUI;
    }
    private void SelectScreen(ScreenSeleced _screen)
    {
        foreach (var i in Screens)
        {
            try
            {
                i.SetActive(false);
            }
            catch { }
        }
        Screens[(int)_screen].SetActive(true);
    }

    public void OpenLoading()
    {
        SelectScreen(ScreenSeleced.Loading);
    }
    void SetEvent()
    {
        BongDaButton.onClick.AddListener(OnClickBongDaButton);
    }

    private void OnClickBongDaButton()
    {
        C_MatchRoom.Instance.CreateMessageToServer(DataOnClient.Instance.room);
        C_MatchSoccer.Instance.CreateMessageToServer(new MatchSoccerMessage(DataOnClient.Instance.room.RoomId));
        SelectScreen(ScreenSeleced.Loading);

    }


    // Start is called before the first frame update
    void Awake()
    {
        AddScreenToArray();
        SetEvent();
        SelectScreen(ScreenSeleced.Room);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
