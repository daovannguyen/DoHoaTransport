using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using TMPro;

public class S_KeepAlive : RegisterEvent
{
    public float KeepAliveTickRate = 10f;
    public float LastKeepAlive;

    private void Update()
    {
        KeepAlive();
    }
    private void KeepAlive()
    {
        if (NetworkManager.Instance.IsServer)
        {
            if (Time.time - LastKeepAlive > KeepAliveTickRate)
            {
                LastKeepAlive = Time.time;
                Server.Instance.BroadCat(new NetKeepAlive());
            }
        }
    }

    #region REGISTEEVENT
    private void OnEnable()
    {
        RegisterEventsServer(ref NetUtility.S_KEEP_ALIVE);
    }
    private void OnDisable()
    {
        UnRegisterEventsServer(ref NetUtility.S_KEEP_ALIVE);
    }
    #endregion

    #region
    public override void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
    }
    #endregion


}
