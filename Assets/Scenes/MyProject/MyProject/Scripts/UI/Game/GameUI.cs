using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoSingleton<GameUI>
{

    private ScreenSeleced currenScreen;

    #region Property
    [Header("Screens")]
    public GameObject LoadingUI;
    public GameObject gameUI;
    public GameObject RuongDoUI;
    public GameObject[] Screens;

    private enum ScreenSeleced
    {
        Loading,
        Game,
        RuongDo,
    }

    //Login variables
    [Header("Game")]
    public Button RuongDo_Btn;

    //Login variables
    [Header("Ruong Do")]
    public Button Close_Btn;

    #endregion

    #region Common
    void SetEvent()
    {
        RuongDo_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.RuongDo); });
        Close_Btn.onClick.AddListener(delegate { SelectScreen(ScreenSeleced.Game); });
    }
    private void AddScreenToArray()
    {
        Screens = new GameObject[10];
        Screens[(int)ScreenSeleced.Loading] = LoadingUI;
        Screens[(int)ScreenSeleced.Game] = gameUI;
        Screens[(int)ScreenSeleced.RuongDo] = RuongDoUI;
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

    private void Awake()
    {
        AddScreenToArray();
        StartCoroutine(WaitSencondsHidenLoading(2));
        SetEvent();
    }
    IEnumerator WaitSencondsHidenLoading(int second)
    {
        SelectScreen(ScreenSeleced.Loading);
        yield return new WaitForSeconds(second);
        SelectScreen(ScreenSeleced.Game);
    }
    #endregion
    public void OpenGameUI()
    {
        SelectScreen(ScreenSeleced.Game);
    }

}
