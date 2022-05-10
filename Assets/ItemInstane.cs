using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemInstane : MonoBehaviour
{
    public Button Item_Btn;
    private int CountItem;
    public TMP_Text count_Text;
    public int indexSpawn = -1;
    public Vector3 scaleChange = new Vector3(100, 100, 100);
    void UpdateView()
    {
        CountItem = DataOnClient.Instance.SpawnCounts[indexSpawn];
        count_Text.text = CountItem.ToString();
    }
    void SetEvent()
    {
        Item_Btn.onClick.AddListener(OnClickItem);
    }

    private void OnClickItem()
    {
        UpdateView();
        Vector3 target = DataOnClient.Instance.GetPlayerLocal().GetComponent<PlayerId>().gobalSpawnTarget;
        C_CreateObject.Instance.CreateMessageToServer(DataOnClient.Instance.room.RoomId, -1, indexSpawn,
           target, Vector3.zero, Vector3.zero);
        C_CountItem.Instance.CreateMessageToServer(DataOnClient.Instance.room.RoomId, indexSpawn, -1);
        GameUI.Instance.OpenGameUI();
    }

    private void Awake()
    {
        CountItem = DataOnClient.Instance.SpawnCounts[indexSpawn];
        UpdateView();
        SetEvent();
    }
    private void Update()
    {
        if (CountItem != DataOnClient.Instance.SpawnCounts[indexSpawn])
        {
            UpdateView();
        }    
    }

}
