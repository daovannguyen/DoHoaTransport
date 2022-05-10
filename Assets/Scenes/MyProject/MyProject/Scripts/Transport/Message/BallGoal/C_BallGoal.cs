using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class C_BallGoal : MonoSingleton<C_BallGoal>
{
    public TMP_Text DisplayTiSo_Text;
    int MyTeam = 0;
    int YourTeam = 0;
    private void UpdateView()
    {
        DisplayTiSo_Text.text = "Đội mình: " + MyTeam.ToString() + "\n"
                              + "Đội bạn :" + YourTeam.ToString();
     }


    #region Register event
    private void OnEnable()
    {
        NetUtility.C_BALLGOAL += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_BALLGOAL -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        BallGoalMessage bpm = JsonUtility.FromJson<BallGoalMessage>((msg as NetBallGoal).ContentBox);

        // chung đội với host
        if (DataOnClient.Instance.room.HostPlayer.Id == DataOnClient.Instance.RoomMatched.HostPlayer.Id)
        {
            MyTeam += bpm.ScoreHost;
            YourTeam += bpm.ScoreNoHost;
        }
        else
        {
            MyTeam += bpm.ScoreNoHost;
            YourTeam += bpm.ScoreHost;
        }
        UpdateView();
    }
    private void Start()
    {
        UpdateView();
    }
    public void CreateMessageToServer(BallGoalMessage bpm)
    {
        NetBallGoal njr = new NetBallGoal();
        njr.ContentBox = JsonUtility.ToJson(bpm);
        Client.Instance.SendToServer(njr);
    }
    #endregion

    #region UI
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Goal")
        {
            Debug.Log("dkhasda");
            if (C_BallPositon.Instance.PlayerIdControl == DataOnClient.Instance.InternalId)
            {
                Debug.Log("dkhasda");
                int ScoreHost = 0;
                int ScoreNoHost = 0;
                if (collision.transform.position.z > 0)
                {
                    ScoreHost = 1;
                }    
                else
                {
                    ScoreNoHost = 1;
                }

                Debug.Log("dkhasda");
                BallGoalMessage bpm = new BallGoalMessage(DataOnClient.Instance.RoomMatched.RoomId,
                    DataOnClient.Instance.InternalId, ScoreHost, ScoreNoHost
                    );

                CreateMessageToServer(bpm);
            }
        }
    }
    #endregion
}
