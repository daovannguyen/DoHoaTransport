using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class C_BallPositon : MonoSingleton<C_BallPositon>
{
    public int PlayerIdControl = -1;
    private Vector3 oldPositon = Vector3.zero;
    private Vector3 oldRotation = Vector3.zero;

    #region Register event
    private void OnEnable()
    {
        NetUtility.C_BALLPOSITION += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_BALLPOSITION -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        BallPositionMessage bpm = JsonUtility.FromJson<BallPositionMessage>((msg as NetBallPosition).ContentBox);
        if (bpm.IdPlayer != DataOnClient.Instance.InternalId)
        {
            transform.position = bpm.Postion;
            transform.eulerAngles = bpm.Rotation;
            PlayerIdControl = bpm.IdPlayer;
        }
        
    }
    public void CreateMessageToServer(BallPositionMessage bpm)
    {
        NetBallPosition njr = new NetBallPosition();
        njr.ContentBox = JsonUtility.ToJson(bpm);
        Client.Instance.SendToServer(njr);
    }
    #endregion

    #region UI
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerIdControl = collision.transform.gameObject.GetComponent<PlayerId>().Id;
        }
        else if (collision.transform.tag == "Goal")
        {

        }
    }
    private void Update()
    {
        if (PlayerIdControl == DataOnClient.Instance.InternalId && (transform.position != oldPositon || transform.eulerAngles != oldRotation))
        {
            oldPositon = transform.position;
            oldRotation = transform.eulerAngles;
            BallPositionMessage bpm = new BallPositionMessage(DataOnClient.Instance.RoomMatched.RoomId, 
                PlayerIdControl, oldPositon, oldRotation
                );

            CreateMessageToServer(bpm);
        }
    }
    #endregion
}
