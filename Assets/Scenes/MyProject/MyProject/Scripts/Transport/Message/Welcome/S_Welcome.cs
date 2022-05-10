using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_Welcome : MonoSingleton<S_Welcome>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_WELCOME += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_WELCOME -= OnEventServer;
    }
    #endregion

    #region Create Reveice 
    private void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        WelcomeMessage wm = new WelcomeMessage(cnn.InternalId);
        NetWelcome nw = new NetWelcome();
        nw.ContentBox = wm.ToJson();
        Server.Instance.SendToClient(cnn, nw);
    }
    #endregion
}
