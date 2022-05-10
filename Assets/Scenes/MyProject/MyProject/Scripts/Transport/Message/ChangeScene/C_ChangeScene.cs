using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_ChangeScene : MonoBehaviour
{

    public Button[] buttons;
    public string[] sceneNames;
    #region Register event
    private void OnEnable()
    {
        NetUtility.C_CHANGESCENE += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_CHANGESCENE -= OnEventClient;
    }
    #endregion

    #region Create Reveice 
    private void OnEventClient(NetMessage msg)
    {
        ChangeSceneMessage sceneName = JsonUtility.FromJson<ChangeSceneMessage>((msg as NetChangeScene).ContentBox);
        SceneManager.LoadScene(sceneName.Name);
    }
    #endregion

    #region UI
    private void OnClickButton(string roomName)
    {
        if (DataOnClient.Instance.IsHost)
        {
            NetChangeScene ncs = new NetChangeScene();
            ncs.ContentBox = JsonUtility.ToJson(new ChangeSceneMessage(DataOnClient.Instance.room.RoomId, roomName));
            Client.Instance.SendToServer(ncs);
        }
    }
    void SetActiveButtons()
    {
        if (!DataOnClient.Instance.IsHost)
        {
            foreach (var i in buttons)
            {
                i.gameObject.SetActive(false);
            }
        }
        else
        {
            try
            {
                buttons[0].onClick.AddListener(delegate { OnClickButton(sceneNames[0]); });
                buttons[1].onClick.AddListener(delegate { OnClickButton(sceneNames[1]); });
                buttons[2].onClick.AddListener(delegate { OnClickButton(sceneNames[2]); });
                buttons[3].onClick.AddListener(delegate { OnClickButton(sceneNames[3]); });
            }
            catch
            {
            }
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        SetActiveButtons();
    }
    #endregion
}
