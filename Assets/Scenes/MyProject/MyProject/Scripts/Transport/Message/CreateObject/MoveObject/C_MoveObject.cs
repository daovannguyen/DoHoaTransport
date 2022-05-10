using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_MoveObject : MonoBehaviour
{

    public GameObject parentObj;

    public float speedTranslate;
    public float speedRotate;
    public float speedTranslateCamera;
    public float speedRotateCamera;
    public GameObject gameObjectControl;
    MoveObjectType controlType;
    private void Awake()
    {
        speedTranslateCamera = 10;
        speedRotateCamera = 30;
        speedTranslate = 0.05f;
        speedRotate = 0.1f;
        controlType = MoveObjectType.TRANSLATE;
    }
    #region UI

    private void ChangeControlType()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            controlType = MoveObjectType.TRANSLATE;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            controlType = MoveObjectType.ROTATE;
        }
    }
    void GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
            CreateMessageToServer(controlType, direction);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
            CreateMessageToServer(controlType, direction);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.right;
            CreateMessageToServer(controlType, direction);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.down;
            CreateMessageToServer(controlType, direction);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.left;
            CreateMessageToServer(controlType, direction);
        }
        if (Input.GetKey(KeyCode.E))
        {
            direction += Vector3.up;
            CreateMessageToServer(controlType, direction);
        }
    }

    Vector3 GetInputTranslationDirectionCamera()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.E))
        {
            direction += Vector3.up;
        }
        return direction;
    }



    private void FixedUpdate()
    {
        gameObjectControl = SelectSpawnObject.Instance.selectObject;
        if (gameObjectControl != null)
        {
            ChangeControlType();
            if (gameObjectControl.name != Camera.main.name)
            {
                GetInputTranslationDirection();
            }
            else
            {
                if (controlType == MoveObjectType.ROTATE)
                {
                    gameObjectControl.transform.Rotate(GetInputTranslationDirectionCamera() * Time.deltaTime * speedTranslateCamera);
                }
                if (controlType == MoveObjectType.TRANSLATE)
                {
                    gameObjectControl.transform.Translate(GetInputTranslationDirectionCamera() * Time.deltaTime * speedRotateCamera);
                }

            }


            //if (controlType == ControlType.ROTATE)
            //{
            //    gameObjectControl.transform.Rotate(GetInputTranslationDirection() * Time.deltaTime * speedRotate);
            //}
        }
    }
    #endregion



    #region Register event
    private void OnEnable()
    {
        NetUtility.C_MOVEOBJECT += OnEventClient;
    }
    private void OnDisable()
    {
        NetUtility.C_MOVEOBJECT -= OnEventClient;
    }
    #endregion

    #region Create Reveice 

    public void CreateMessageToServer(MoveObjectType type, Vector3 target)
    {
        int id = SelectSpawnObject.Instance.selectObject.GetComponent<ObjectId>().Id;
        MoveObjectMessage moveTransport = new MoveObjectMessage(DataOnClient.Instance.room.RoomId, id, type, target);
        NetMoveObject netMoveObject = new NetMoveObject();
        netMoveObject.ContentBox = JsonUtility.ToJson(moveTransport);
        Client.Instance.SendToServer(netMoveObject);
    }
    public void OnEventClient(NetMessage msg)
    {
        NetMoveObject mmo = msg as NetMoveObject;
        MoveObjectMessage mom = JsonUtility.FromJson<MoveObjectMessage>(mmo.ContentBox);
        gameObjectControl = DataOnClient.Instance.SpawnGameObjects[mom.Id];

        if (mom.Type == MoveObjectType.TRANSLATE)
        {
            gameObjectControl.transform.Translate(mom.Target * speedTranslate);
        }
        if (mom.Type == MoveObjectType.ROTATE)
        {
            gameObjectControl.transform.Rotate(mom.Target * speedRotate);
        }
    }
    #endregion
}
