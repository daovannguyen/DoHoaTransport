using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_KeepAlive : RegisterEvent
{
    #region REGISTEEVENT
    private void OnEnable()
    {
        RegisterEventsClient(ref NetUtility.C_KEEP_ALIVE);
    }
    private void OnDisable()
    {
        UnRegisterEventsClient(ref NetUtility.C_KEEP_ALIVE);
    }
    #endregion

    public override void OnEventClient(NetMessage msg)
    {
        Client.Instance.SendToServer(msg);
    }
}
