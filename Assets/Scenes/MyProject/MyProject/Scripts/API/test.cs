using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData a = new PlayerData("Set", "Van", "Nguyen");
        //StartCoroutine(APICall.JsonPost(PathASP.PLAYERDATA, a.ToJson(), LoginSuccessly, LoginError));
    }

    private void LoginError(UnityWebRequest arg1, string arg2)
    {
        Debug.Log("sai");
    }

    private void LoginSuccessly(UnityWebRequest w, string json)
    {
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
