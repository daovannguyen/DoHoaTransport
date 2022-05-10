using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L_ChangeScene : RegisterEvent
{
    //public Button[] buttons;
    //public string[] sceneNames;
    //private class SceneName
    //{
    //    public string name;
    //    public SceneName()
    //    {
    //        name = "";
    //    }
    //    public SceneName(string _name)
    //    {
    //        name = _name;
    //    }
    //}
    //void SetActiveButtons()
    //{
    //    if (!NetworkManager.Instance.IsHost)
    //    {
    //        foreach (var i in buttons)
    //        {
    //            i.gameObject.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        try
    //        {
    //            buttons[0].onClick.AddListener(delegate { OnClickButton(sceneNames[0]); });
    //            buttons[1].onClick.AddListener(delegate { OnClickButton(sceneNames[1]); });
    //            buttons[2].onClick.AddListener(delegate { OnClickButton(sceneNames[2]); });
    //            buttons[3].onClick.AddListener(delegate { OnClickButton(sceneNames[3]); });
    //        }
    //        catch
    //        {
    //        }
    //    }
    //}
    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    SetActiveButtons();
    //}

    //private void OnClickButton(string roomName)
    //{
    //    if (NetworkManager.Instance.IsHost)
    //    {
    //        NetChangeScene ncs = new NetChangeScene();
    //        ncs.ContentBox = JsonUtility.ToJson(new SceneName(roomName));
    //        Client.Instance.SendToServer(ncs);

    //    }
    //}
    //private void OnEnable()
    //{
    //    RegisterEvents(ref NetUtility.S_CHANGESCENE, ref NetUtility.C_CHANGESCENE);
    //}
    //private void OnDisable()
    //{
    //    UnRegisterEvents(ref NetUtility.S_CHANGESCENE, ref NetUtility.C_CHANGESCENE);
    //}
    //public override void OnEventClient(NetMessage obj)
    //{
    //    SceneName sceneName = JsonUtility.FromJson<SceneName>((obj as NetChangeScene).ContentBox);
    //    SceneManager.LoadScene(sceneName.name);
    //}
    //public override void OnEventServer(NetMessage msg, NetworkConnection cnn)
    //{
    //    Server.Instance.BroadCatOnRoom(msg,1);
    //}

}
