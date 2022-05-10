using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CountItem : MonoSingleton<C_CountItem>
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.C_COUNTITEM += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_COUNTITEM -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(int idRoom, int indexPrefab, int countChange)
    {
        CountItemMessage scm = new CountItemMessage(idRoom, indexPrefab, countChange);
        NetCountItem nco = new NetCountItem();
        nco.ContentBox = JsonUtility.ToJson(scm);
        Client.Instance.SendToServer(nco);
    }
    public void OnEventClient(NetMessage msg)
    {
        CountItemMessage com = JsonUtility.FromJson<CountItemMessage>((msg as NetCountItem).ContentBox);
        DataOnClient.Instance.SpawnCounts[com.IndexPrefab] = DataOnClient.Instance.SpawnCounts[com.IndexPrefab] + com.CountChange;
    }
    #endregion
}
