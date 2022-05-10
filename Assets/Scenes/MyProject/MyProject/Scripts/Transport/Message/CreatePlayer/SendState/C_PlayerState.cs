using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_PlayerState : MonoSingleton<C_PlayerState>
{
    MessageType messageType;


    #region Register event
    private void OnEnable()
    {
        NetUtility.C_PLAYERSTATE += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_PLAYERSTATE -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    // player state chứa mỗi string state
    public void CreateMessageToServer(PlayerStateMessage psm)
    {
        psm.messageType = messageType;
        psm.Id = DataOnClient.Instance.InternalId;
        if (messageType == MessageType.TEAMONE)
        {
            psm.IdRoom = DataOnClient.Instance.room.RoomId;
        }
        else if (messageType == MessageType.TEAMTWO)
        {
            psm.IdRoom = DataOnClient.Instance.RoomMatched.RoomId;
        }
        NetPlayerState nps = new NetPlayerState();
        nps.ContentBox = JsonUtility.ToJson(psm);
        Client.Instance.SendToServer(nps);
    }
    public void OnEventClient(NetMessage msg)
    {
        NetPlayerState nps = msg as NetPlayerState;
        PlayerStateMessage ps = JsonUtility.FromJson<PlayerStateMessage>(nps.ContentBox);
        GameObject player = DataOnClient.Instance.GetPlayerWithId(ps.Id);
        Animator animator = player.GetComponent<Animator>();
        animator.SetTrigger(ps.Trigger);


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
    private void Awake()
    {
        SetMessageType();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CreateMessageToServer(new PlayerStateMessage("Run"));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CreateMessageToServer(new PlayerStateMessage("Run"));
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            CreateMessageToServer(new PlayerStateMessage("Normal"));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            CreateMessageToServer(new PlayerStateMessage("Normal"));
        }
    }
    #endregion
}
