using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeCountDown : MonoBehaviour
{
    private TMP_Text countDownTime_Text;
    public int secondsLeft = 4;
    public string sceneName = SceneName.GAMEXEPHINH;
    private bool takingAway = false;
    void SetSceneName()
    {
        if (SceneManager.GetActiveScene().name == SceneName.SELECTCHARACTOR)
        {
            sceneName = SceneName.GAMEXEPHINH;
        }
        else if (SceneManager.GetActiveScene().name == SceneName.GAMESOCCER)
        {
            sceneName = SceneName.ROOM;
        }
    }
    private void Awake()
    {
        countDownTime_Text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSceneName();
        countDownTime_Text.text = "00:" + secondsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimeTake());
        }  
        if (secondsLeft <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator TimeTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        countDownTime_Text.text = "00:" + secondsLeft;
        takingAway = false;
    }
}
