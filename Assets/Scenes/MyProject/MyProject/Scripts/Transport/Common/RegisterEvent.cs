using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class RegisterEvent : MonoSingleton<RegisterEvent>
{
    public void RegisterEvents(ref Action<NetMessage, NetworkConnection> _S_Event, ref Action<NetMessage> _C_Event)
    {
        _S_Event += OnEventServer;
        _C_Event += OnEventClient;
    }
    public void UnRegisterEvents(ref Action<NetMessage, NetworkConnection> _S_Event, ref Action<NetMessage> _C_Event)
    {
        _S_Event -= OnEventServer;
        _C_Event -= OnEventClient;
    }
    public void RegisterEventsClient(ref Action<NetMessage> _C_Event)
    {
        _C_Event += OnEventClient;
    }
    public void RegisterEventsServer(ref Action<NetMessage, NetworkConnection> _S_Event)
    {

        _S_Event += OnEventServer;
    }
    public void UnRegisterEventsClient(ref Action<NetMessage> _C_Event)
    {
        _C_Event -= OnEventClient;
    }
    public void UnRegisterEventsServer(ref Action<NetMessage, NetworkConnection> _S_Event)
    {

        _S_Event -= OnEventServer;
    }


    public virtual void OnEventServer(NetMessage arg1, NetworkConnection arg2)
    {
    }

    public virtual void OnEventClient(NetMessage obj)
    {
    }


}
