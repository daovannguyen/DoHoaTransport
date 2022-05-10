using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CreateObject : MonoSingleton<C_CreateObject>
{
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_CREATEOBJECT += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_CREATEOBJECT -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(int idRoom, int id, int indexPrefab, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        CreateObjectMessage scm = new CreateObjectMessage(idRoom, id, indexPrefab, position, rotation, scale);
        NetCreateObject nco = new NetCreateObject();
        nco.ContentBox = JsonUtility.ToJson(scm);
        Client.Instance.SendToServer(nco);
    }
    public void OnEventClient(NetMessage msg)
    {
        CreateObjectMessage com = JsonUtility.FromJson<CreateObjectMessage>((msg as NetCreateObject).ContentBox);
        DataOnClient.Instance.SetSpawn(com);
        DataOnClient.Instance.SpawnCounts[com.IndexPrefab] = DataOnClient.Instance.SpawnCounts[com.IndexPrefab];
    }
    #endregion
}
