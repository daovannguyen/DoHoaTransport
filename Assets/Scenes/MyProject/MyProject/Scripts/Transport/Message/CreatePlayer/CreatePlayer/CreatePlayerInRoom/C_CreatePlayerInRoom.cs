using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CreatePlayerInRoom : MonoSingleton<C_CreatePlayerInRoom>
{

    #region Register event
    private void OnEnable()
    {
        NetUtility.C_CREATEPLAYERINROOM += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_CREATEPLAYERINROOM -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(int idRoom, int id, string displayName, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        CreatePlayerInRoomMessage scm = new CreatePlayerInRoomMessage(idRoom, id, displayName, position, rotation, scale);
        NetCreatePlayerInRoom nco = new NetCreatePlayerInRoom();
        nco.ContentBox = JsonUtility.ToJson(scm);
        Client.Instance.SendToServer(nco);
    }
    public void OnEventClient(NetMessage msg)
    {
        CreatePlayerInRoomMessage com = JsonUtility.FromJson<CreatePlayerInRoomMessage>((msg as NetCreatePlayerInRoom).ContentBox);
        //DataOnClient.Instance.SetPlayerRoomMatched(com);
    }
    #endregion

    private void Start()
    {
        StartCoroutine(IntancePlayer());
    }

    IEnumerator IntancePlayer()
    {
        yield return new WaitForSeconds(1);
        Vector3 randomPosition = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        CreateMessageToServer(DataOnClient.Instance.RoomMatched.RoomId, -1,
            DataOnClient.Instance.playerData.DisplayName, randomPosition, Vector3.zero, Vector3.zero);
    }
}
