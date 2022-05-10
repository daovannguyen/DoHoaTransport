using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Welcome : MonoSingleton<C_Welcome>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_WELCOME += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_WELCOME -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        WelcomeMessage scm = new WelcomeMessage().FromJson((msg as NetWelcome).ContentBox);
        DataOnClient.Instance.InternalId = scm.InternalId;
    }
    #endregion
    private void Awake()
    {
        Client.Instance.SendToServer(new NetWelcome());
    }
}
