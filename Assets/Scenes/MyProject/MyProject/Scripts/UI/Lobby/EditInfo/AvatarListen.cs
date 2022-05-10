using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarListen : MonoBehaviour
{
    public GameObject[] avatar = new GameObject[2];
    public Image[] avatarImage = new Image[2];
    private void OnEnable()
    {
        EventManager.ReceivePlayerData += OnChangeImage;
    }
    private void OnDisable()
    {
        EventManager.ReceivePlayerData -= OnChangeImage;
    }

    private void InitAvatarImage()
    {
        for(int i=0; i<avatarImage.Length; i++)
        {
            avatarImage[i] = avatar[i].GetComponent<Image>();
        }
    }
    public void OnChangeImage(bool obj)
    {
        string base64 = DataOnClient.Instance.playerData.Avatar;
        if (base64.Length > 100)
        {
            byte[] avatarBytes = Convert.FromBase64String(base64);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(avatarBytes);
            foreach (var i in avatarImage)
            {
                i.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f); ;
            }
        }
    }
    private void Awake()
    {
        InitAvatarImage();
    }
}
