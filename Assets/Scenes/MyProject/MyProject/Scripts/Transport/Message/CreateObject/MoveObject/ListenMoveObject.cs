using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;


public class ListenMoveObject : MonoBehaviour
{
    //public GameObject parentObj;
    
    //public float speedTranslate;
    //public float speedRotate;
    //public float speedTranslateCamera;
    //public float speedRotateCamera;
    //public GameObject gameObjectControl;
    //ControlType controlType;
    //private void Awake()
    //{
    //    speedTranslateCamera = 10;
    //    speedRotateCamera = 30;
    //    speedTranslate = 0.05f;
    //    speedRotate = 0.1f;
    //    controlType = ControlType.TRANSLATE;
    //}
    //private void Start()
    //{
    //}
    //private void ChangeControlType()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        controlType = ControlType.TRANSLATE;
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        controlType = ControlType.ROTATE;
    //    }
    //}
    //void GetInputTranslationDirection()
    //{
    //    Vector3 direction = new Vector3();
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        direction += Vector3.forward;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        direction += Vector3.back;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        direction += Vector3.right;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        direction += Vector3.down;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        direction += Vector3.left;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        direction += Vector3.up;
    //        SendMoveObject(controlType, gameObjectControl.name, direction);
    //    }
    //}

    //Vector3 GetInputTranslationDirectionCamera()
    //{
    //    Vector3 direction = new Vector3();
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        direction += Vector3.forward;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        direction += Vector3.back;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        direction += Vector3.right;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        direction += Vector3.down;
    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        direction += Vector3.left;
    //    }
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        direction += Vector3.up;
    //    }
    //    return direction;
    //}


    //void SendMoveObject(ControlType _controlType, string _nameObject, Vector3 _target)
    //{
    //    MoveTransport moveTransport = new MoveTransport(_controlType, _nameObject, _target);
    //    NetMoveObject netMoveObject = new NetMoveObject();
    //    netMoveObject.ContentBox = JsonUtility.ToJson(moveTransport);
    //    Client.Instance.SendToServer(netMoveObject);
    //}
    //private void FixedUpdate()
    //{
    //    gameObjectControl = SelectObject.Instance.selectObject;
    //    if (gameObjectControl != null)
    //    {
    //        ChangeControlType();
    //        if (gameObjectControl.name != Camera.main.name)
    //        {
    //            GetInputTranslationDirection();
    //        }
    //        else
    //        {
    //            if (controlType == ControlType.ROTATE)
    //            {
    //                gameObjectControl.transform.Rotate(GetInputTranslationDirectionCamera() * Time.deltaTime * speedTranslateCamera);
    //            }
    //            if (controlType == ControlType.TRANSLATE)
    //            {
    //                gameObjectControl.transform.Translate(GetInputTranslationDirectionCamera() * Time.deltaTime * speedRotateCamera);
    //            }

    //        }


    //        //if (controlType == ControlType.ROTATE)
    //        //{
    //        //    gameObjectControl.transform.Rotate(GetInputTranslationDirection() * Time.deltaTime * speedRotate);
    //        //}
    //    }
    //}

    //public void OnEventClient(NetMessage obj)
    //{
    //    NetMoveObject mmo = obj as NetMoveObject;
    //    MoveTransport moveTransport = JsonUtility.FromJson<MoveTransport>(mmo.ContentBox);
    //    gameObjectControl = parentObj.transform.Find(moveTransport.name).gameObject;

    //    if (moveTransport.controlType == ControlType.TRANSLATE)
    //    {
    //        gameObjectControl.transform.Translate(moveTransport.target * speedTranslate);
    //    }
    //    if (moveTransport.controlType == ControlType.ROTATE)
    //    {
    //        gameObjectControl.transform.Rotate(moveTransport.target * speedRotate);
    //    }
    //}
    //public void OnEventServer(NetMessage msg, NetworkConnection cnn)
    //{
    //    Server.Instance.BroadCat(msg);
    //}


    //public void RegisterEvents()
    //{
    //    NetUtility.S_MOVEOBJECT += OnEventServer;
    //    NetUtility.C_MOVEOBJECT += OnEventClient;
    //}

    //public void UnRegisterEvents()
    //{
    //    NetUtility.S_MOVEOBJECT -= OnEventServer;
    //    NetUtility.C_MOVEOBJECT -= OnEventClient;
    //}
    //private void OnEnable()
    //{
    //    RegisterEvents();
    //}
    //private void OnDisable()
    //{
    //    UnRegisterEvents();
    //}
}
