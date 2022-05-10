using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public string IpAdress;
    public int Port;
    public bool IsHost;
    public int InternalId;
    public int indexPlayerPrefab;

    public bool IsServer;
    public string Hello;
    public Server server;
    public Client client;

    private void SetIpAdressAndPort()
    {
        IpAdress = "127.0.0.1";
        Port = 8007;
    }
    private void Awake()
    {
        SetIpAdressAndPort();
        DontDestroyOnLoad(this);
    }
    public void SetServer()
    {
        IsServer = true;
        Hello = "server";
    }
    public void SetClient()
    {
        Hello = "clinet";
        IsServer = false;
    }
}
