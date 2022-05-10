using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAllow : MonoBehaviour
{
    public GameObject CameraAllowObj;

    private void Update()
    {
        Camera.main.transform.LookAt(CameraAllowObj.transform.position);
        Camera.main.transform.position = CameraAllowObj.transform.position;
        Camera.main.transform.eulerAngles = CameraAllowObj.transform.eulerAngles;

    }

}
