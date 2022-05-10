using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginAsClientOrServer : MonoBehaviour
{
    public Button Server_Btn;
    public Button Client_Btn;
    private string IpAddress;
    private ushort Port;
    // Update is called once per frame
    void Awake()
    {
        IpAddress = "127.0.0.1";
        Port = 8000;
        //new UnityLogger;
        Application.targetFrameRate = 60;

        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-launch-as-client")
            {
                OnClient();
            }
            else if (args[i] == "-launch-as-server")
            {
                OnServer();
            }
        }


        Client_Btn.onClick.AddListener(OnClient);
        Server_Btn.onClick.AddListener(OnServer);
    }
    public void OnClient()
    {
        Client.Instance.Init(IpAddress, Port);
        NetworkManager.Instance.SetClient();
        AfterClickButton();
    }
    public void OnServer()
    {
        Server.Instance.Init(Port);
        Client.Instance.Init(IpAddress, Port);
        NetworkManager.Instance.SetServer();
        AfterClickButton();
    }

    void AfterClickButton()
    {
        SceneManager.LoadScene("Login");
    }
}
