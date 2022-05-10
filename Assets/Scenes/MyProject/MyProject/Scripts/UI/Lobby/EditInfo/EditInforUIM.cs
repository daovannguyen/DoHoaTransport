using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class EditInforUIM : MonoBehaviour
{
    public TMP_InputField displayName_Input;
    public TMP_InputField khoa_Input;
    public TMP_InputField queQuan_Input;
    public TMP_InputField dienThoai_Input;
    public List<TMP_InputField> inputFields;

    public TMP_Text warming_Text;

    public Button brower_Btn;
    public Button edit_Btn;
    public Button update_Btn;

    public Image avatar_Image;
    public Texture2D dragHereTexture;
    void InitListInputField()
    {
        inputFields = new List<TMP_InputField>();
        inputFields.Add(displayName_Input);
        inputFields.Add(khoa_Input);
        inputFields.Add(queQuan_Input);
        inputFields.Add(dienThoai_Input);
    }
    void SetOnlyReadListInput(bool display)
    {
        foreach(var i in inputFields)
        {
            i.readOnly = display;
        }
    }
    private void Awake()
    {
        Init();
        InitListInputField();
        DisplayOnModeView();
    }
    private void SetPropertyWarmingText(TMP_Text _Text, bool isActve, string message = "")
    {
        _Text.gameObject.SetActive(isActve);
        _Text.text = message;
    }
    void Init()
    {
        displayName_Input.text = DataOnClient.Instance.playerData.DisplayName;
        khoa_Input.text = DataOnClient.Instance.playerData.Khoa;
        queQuan_Input.text = DataOnClient.Instance.playerData.QueQuan;
        dienThoai_Input.text = DataOnClient.Instance.playerData.DienThoai;




        edit_Btn.onClick.AddListener(DisplayOnModeChange);
        update_Btn.onClick.AddListener(OnClickUpdateButton);
        brower_Btn.onClick.AddListener(OnClickBrowerButton);
    }
    #region Send Update
    void OnClickUpdateButton()
    {
        DataOnClient.Instance.playerData.DisplayName = displayName_Input.text;
        DataOnClient.Instance.playerData.Khoa = khoa_Input.text;
        DataOnClient.Instance.playerData.QueQuan = queQuan_Input.text;
        DataOnClient.Instance.playerData.DienThoai = dienThoai_Input.text;
        StartCoroutine(APICall.JsonPost(PathASP.PLAYERDATA, JsonUtility.ToJson(DataOnClient.Instance.playerData.ToUpdate()),
            OnSuccess, OnError
            ));
    }

    private void OnError(UnityWebRequest arg1, string arg2)
    {

    }

    private void OnSuccess(UnityWebRequest arg1, string arg2)
    {
        EventManager.ReceivePlayerData?.Invoke(true);
        SetPropertyWarmingText(warming_Text, true, "Cap nhat thanh cong!");
        DisplayOnModeView();
    }

    #endregion
    void OnClickBrowerButton()
    {
        System.Diagnostics.Process.Start("explorer.exe");
        avatar_Image.sprite = Sprite.Create(dragHereTexture, new Rect(0.0f, 0.0f,
            dragHereTexture.width, dragHereTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
    public void DisplayOnModeView()
    {
        SetOnlyReadListInput(true);
        edit_Btn.gameObject.SetActive(true);
        update_Btn.gameObject.SetActive(false);
        brower_Btn.gameObject.SetActive(false);
        //Congratulations_Img.gameObject.SetActive(false);
    }
    void DisplayOnModeChange()
    {
        SetOnlyReadListInput(false);
        SetPropertyWarmingText(warming_Text, true, "");
        edit_Btn.gameObject.SetActive(false);
        update_Btn.gameObject.SetActive(true);
        brower_Btn.gameObject.SetActive(true);
    }
}
