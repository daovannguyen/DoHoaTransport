using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class LobbyUIManager : MonoSingleton<LobbyUIManager>
{
    #region Property

    [Header("Screens")]
    public GameObject LobbyUI;
    public GameObject EditInfoUI;
    public GameObject LoadingUI;
    public GameObject CreateRoomUI;
    public GameObject SettingUI;
    public GameObject[] Screens; 
    private enum ScreenSeleced
    {
        Lobby = 0,
        EditInfo,
        Loading,
        CreateRoom,
        Setting
    }


    //Login variables
    [Header("Lobby")]
    public Button LAvatar_Btn;
    public Button LSetting_Btn;
    public Button LCreateRoom_Btn;
    // phan tin nhan lobby
    public TMP_Text LChat_Text;
    public TMP_InputField LChat_Input;


    //Register variables
    [Header("EditInfo")]
    public Button EBack_Btn;


    [Header("CreateRoom")]
    public Button CBack_Btn;


    [Header("Setting")]
    public Button SBack_Btn;
    #endregion

    #region Common
    private void AddScreenToArray()
    {
        Screens = new GameObject[10];
        Screens[(int)ScreenSeleced.Lobby] = LobbyUI;
        Screens[(int)ScreenSeleced.Setting] = SettingUI;
        Screens[(int)ScreenSeleced.Loading] = LoadingUI;
        Screens[(int)ScreenSeleced.EditInfo] = EditInfoUI;
        Screens[(int)ScreenSeleced.CreateRoom] = CreateRoomUI;
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
    void SetEvent()
    {
        LAvatar_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.EditInfo); });
        LSetting_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Setting); });
        LCreateRoom_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.CreateRoom); });
        EBack_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Lobby); });
        CBack_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Lobby); });
        SBack_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Lobby); });

    }
    private void Awake()
    {
        AddScreenToArray();
        SetEvent();
        StartCoroutine(WaitSencondsHidenLoading(2));
        GetImageBase64ToPlayerData();
        LobbyUIM.Instance.SetUpLobby();
    }
    #endregion
    #region Get Image Avatar
    private void GetImageBase64ToPlayerData()
    {
        StartCoroutine(APICall.JsonPost(PathASP.AVATAR, JsonUtility.ToJson(DataOnClient.Instance.playerData.ToGet()),
            OnSuccess, OnError
            ));
    }

    private void OnError(UnityWebRequest arg1, string arg2)
    {
    }

    private void OnSuccess(UnityWebRequest arg1, string arg2)
    {
        DataOnClient.Instance.playerData = JsonUtility.FromJson<PlayerData>(arg2);
        EventManager.ReceivePlayerData?.Invoke(true);
    }
    #endregion

    public void SetLobby()
    {
        SelectScreen(ScreenSeleced.Loading);
    }
    IEnumerator WaitSencondsHidenLoading(int second)
    {
        SelectScreen(ScreenSeleced.Loading);
        yield return new WaitForSeconds(second);
        SelectScreen(ScreenSeleced.Lobby);
    }
}
