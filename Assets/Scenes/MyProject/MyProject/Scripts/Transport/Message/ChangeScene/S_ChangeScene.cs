using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class S_ChangeScene : MonoBehaviour
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.S_CHANGESCENE += OnEventServer;
    }
    private void OnDisable()
    {
        NetUtility.S_CHANGESCENE -= OnEventServer;
    }
    #endregion

    #region Create Reveice 
    private void OnEventServer(NetMessage msg, NetworkConnection cnn)
    {
        ChangeSceneMessage csm = JsonUtility.FromJson<ChangeSceneMessage>((msg as NetChangeScene).ContentBox);
        Server.Instance.BroadCatOnRoom(msg, csm.IdRoom);
    }
    #endregion
}
