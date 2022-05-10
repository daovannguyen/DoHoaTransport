using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LoginUIManager : MonoSingleton<LoginUIManager>
{
    private ScreenSeleced currenScreen;

    #region Property
    [Header("Screens")]
    public GameObject LoginUI;
    public GameObject RegisterUI;
    public GameObject LoadingUI;
    public GameObject[] Screens;

    private enum ScreenSeleced
    {
        Login,
        Register,
        Loading
    }


    //Login variables
    [Header("Login")]
    public TMP_InputField LUsername_Input;
    public TMP_InputField LPassword_Input;
    public Button LLogin_Btn;
    public Button LRegister_Btn;
    public TMP_Text LWarming_Text;

    //Register variables
    [Header("Register")]
    public TMP_InputField RUsername_Input;
    public TMP_InputField RPassword_Input;
    public TMP_InputField RPasswordVerify_Input;
    public Button RRegister_Btn;
    public Button RBack_Btn;
    public TMP_Text RWarning_Text;
    #endregion

    #region Common
    private void AddScreenToArray()
    {
        Screens = new GameObject[10];
        Screens[(int)ScreenSeleced.Login] = LoginUI;
        Screens[(int)ScreenSeleced.Register] = RegisterUI;
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
    private void HiddenWarmingText(bool isHiden)
    {
        SetPropertyWarmingText(LWarming_Text, false);
        SetPropertyWarmingText(RWarning_Text, false);
    }
    private void SetPropertyWarmingText(TMP_Text _Text, bool isActve, string message = "")
    {
        _Text.gameObject.SetActive(isActve);
        _Text.text = message;
    }

    void SetEvent()
    {
        LRegister_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Register); });
        RBack_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Login); });
        LLogin_Btn.onClick.AddListener(OnClickLLogin);
        RRegister_Btn.onClick.AddListener(OnClickRRigister);
    }
    private void Awake()
    {
        AddScreenToArray();
        SelectScreen(ScreenSeleced.Login);
        HiddenWarmingText(true);
        SetEvent();
    }
    #endregion

    #region Regiter
    private void OnClickRRigister()
    {
        if (CheckRegisterField())
        {
            GetPlayerData.Instance.SendToASP("Set", RUsername_Input.text, RPassword_Input.text);
            currenScreen = ScreenSeleced.Register;
            SelectScreen(ScreenSeleced.Loading);
        }
    }
    private bool CheckRegisterField()
    {
        if (RUsername_Input.text == "" || RPassword_Input.text == "")
        {
            SetPropertyWarmingText(RWarning_Text, true, "Chưa điền email hoặc mật khẩu!");
            return false;
        }
        else if (RPassword_Input.text != RPasswordVerify_Input.text)
        {
            SetPropertyWarmingText(RWarning_Text, true, "Xác nhận mật khẩu không đúng!");
            return false;
        }
        SetPropertyWarmingText(RWarning_Text, false);
        return true;
    }

    #endregion

    #region Login
    private void OnClickLLogin()
    {
        if (CheckLoginField())
        {
            GetPlayerData.Instance.SendToASP("Get", LUsername_Input.text, LPassword_Input.text);
            currenScreen = ScreenSeleced.Login;
            SelectScreen(ScreenSeleced.Loading);
        }
    }
    private bool CheckLoginField()
    {
        if (LUsername_Input.text == "" || LPassword_Input.text == "")
        {
            SetPropertyWarmingText(LWarming_Text, true, "Chưa điền email hoặc mật khẩu!");
            return false;
        }
        SetPropertyWarmingText(LWarming_Text, false);
        return true;
    }
    #endregion

    private void OnEnable()
    {
        EventManager.ReceivePlayerData += OnHandleReceiveData;
    }
    private void OnDisable()
    {
        EventManager.ReceivePlayerData -= OnHandleReceiveData;
    }

    private void OnHandleReceiveData(bool obj)
    {
        SelectScreen(currenScreen);
        if (obj)
        {
            if (currenScreen == ScreenSeleced.Login)
            {
                SceneManager.LoadScene("Lobby");
            }
            else if (currenScreen == ScreenSeleced.Register)
            {
                SelectScreen(ScreenSeleced.Login);
                SetPropertyWarmingText(LWarming_Text, false);
                SetPropertyWarmingText(RWarning_Text, false);
            }
        }
        else
        {
            if (currenScreen == ScreenSeleced.Login)
            {
                SetPropertyWarmingText(LWarming_Text, true, "Tài khoản không đúng!");
            }
            else if (currenScreen == ScreenSeleced.Register)
            {
                SetPropertyWarmingText(RWarning_Text, true, "Tài khoản đã tồn tại!");
            }
        }
    }
}
