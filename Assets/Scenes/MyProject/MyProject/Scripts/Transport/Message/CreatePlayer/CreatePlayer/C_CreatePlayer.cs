using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_CreatePlayer : MonoBehaviour
{
    MessageType messageType;
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_CREATEPLAYER += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_CREATEPLAYER -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(CreatePlayerMessage scm)
    {
        NetCreatePlayer nco = new NetCreatePlayer();
        nco.ContentBox = JsonUtility.ToJson(scm);
        Client.Instance.SendToServer(nco);
    }
    public void OnEventClient(NetMessage msg)
    {
        CreatePlayerMessage com = JsonUtility.FromJson<CreatePlayerMessage>((msg as NetCreatePlayer).ContentBox);
        if (com.messageType == MessageType.TEAMONE)
        {
            DataOnClient.Instance.SetPlayer(com);
        }
        else if (com.messageType == MessageType.TEAMTWO)
        {
            DataOnClient.Instance.SetPlayerRoomMatched(com);
        }
    }
    #endregion

    #region UI

    void SetMessageType()
    {
        if (SceneManager.GetActiveScene().name == SceneName.GAMEXEPHINH)
        {
            messageType = MessageType.TEAMONE;
        }
        else if (SceneManager.GetActiveScene().name == SceneName.GAMESOCCER)
        {
            messageType = MessageType.TEAMTWO;
        }
    }

    private int GetRoomId()
    {
        if (messageType == MessageType.TEAMONE)
        {
            return DataOnClient.Instance.room.RoomId;
        }
        else if (messageType == MessageType.TEAMTWO)
        {
            return DataOnClient.Instance.RoomMatched.RoomId;
        }
        else return -1;
    }
    private void Awake()
    {
        SetMessageType();
    }

    private void Start()
    {
        StartCoroutine(IntancePlayer());
    }

    IEnumerator IntancePlayer()
    {
        yield return new WaitForSeconds(1);
        int indexPrefab = DataOnClient.Instance.indexPlayerPrefab;
        Vector3 randomPosition = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        CreatePlayerMessage cpm = new CreatePlayerMessage();
        cpm = new CreatePlayerMessage(messageType, GetRoomId(), -1, indexPrefab,
            DataOnClient.Instance.playerData.DisplayName, randomPosition, Vector3.zero, Vector3.zero);
        CreateMessageToServer(cpm);
    }
    private void Update()
    {
    }
    #endregion
}
