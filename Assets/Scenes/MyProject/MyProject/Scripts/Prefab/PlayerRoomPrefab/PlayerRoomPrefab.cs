using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class PlayerRoomPrefab : MonoBehaviour
{
    public string DisplayName;
    public string Avatar;
    

    public Image avatar_Image;
    public TMP_Text displayName_Text;
    int dem = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dem == 0 && DisplayName != "" && Avatar != "")
        {
            displayName_Text.text = DisplayName;
            StartCoroutine(APICall.JsonPost(PathASP.GETAVATAR, Avatar, OnSuccess, OnError));
            dem += 1;
        }
    }

    private void OnError(UnityWebRequest arg1, string arg2)
    {
    }

    private void OnSuccess(UnityWebRequest arg1, string arg2)
    {
        string base64 = arg2;
        byte[] avatarBytes = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(avatarBytes);
        avatar_Image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f); 
    }
}
