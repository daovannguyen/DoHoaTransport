using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_MovePlayer : MonoBehaviour
{
    MessageType messageType;
    float speed = 5f;
    float rotationSpeed = 1f;

    #region Register event
    private void OnEnable()
    {
        NetUtility.C_MOVEPLAYER += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_MOVEPLAYER -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    public void CreateMessageToServer(MovePlayerMessage mpm)
    {
        NetMovePlayer nsc = new NetMovePlayer();
        nsc.ContentBox = JsonUtility.ToJson(mpm);
        Client.Instance.SendToServer(nsc);
    }
    private void OnEventClient(NetMessage msg)
    {
        MovePlayerMessage scm = JsonUtility.FromJson<MovePlayerMessage>((msg as NetMovePlayer).ContentBox);
        //player.GetComponent<Rigidbody>().velocity = (player.transform.forward * scm.vAxist);
        //player.transform.Rotate(player.transform.up * scm.hAxist);
        if (scm.Id != DataOnClient.Instance.InternalId)
        {
            GameObject player = DataOnClient.Instance.PlayerGameObjects[scm.Id];
            player.transform.position = scm.Positon;
            player.transform.eulerAngles = scm.Rotation;
        }
    }
    #endregion

    #region Work With UI

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
    private void Awake()
    {
        SetMessageType();
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

    private void FixedUpdate()
    {
        float hAxis = 0;
        float vAxis = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vAxis = speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vAxis = -speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            hAxis = -rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hAxis = +rotationSpeed;
        }
        if (Mathf.Abs(hAxis) > 0 || Mathf.Abs(vAxis) > 0)
        {
            GameObject player = DataOnClient.Instance.GetPlayerLocal();
            player.GetComponent<Rigidbody>().velocity = (player.transform.forward * vAxis);
            player.transform.Rotate(player.transform.up * hAxis);
            MovePlayerMessage mpm = new MovePlayerMessage(GetRoomId(),
                DataOnClient.Instance.InternalId, player.transform.position, player.transform.eulerAngles
                );
            CreateMessageToServer(mpm);
        }
    }
    #endregion
}
