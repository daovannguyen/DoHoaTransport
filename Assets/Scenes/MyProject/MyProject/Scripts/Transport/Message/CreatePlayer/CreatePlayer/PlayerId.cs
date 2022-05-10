using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerId: MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text displayName_Text;
    [HideInInspector]
    public string displayName;
    [HideInInspector]
    public int Id;
    [HideInInspector]
    public Vector3 gobalSpawnTarget;
    public GameObject spawnHere;
    public GameObject CameraPosition;


    //public Rigidbody _rigidbody;

    //float speed = 200f;
    //float rotationSpeed = 100f;
    //public float hAxis;
    //public float vAxis;

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Player";
        displayName_Text.text = displayName;
        //_rigidbody = GetComponent<Rigidbody>();
    }

    public void SetId(int id)
    {
        Id = id;
    }
    public int GetId()
    {
        return Id;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
        gobalSpawnTarget = spawnHere.transform.transform.position;
        if (displayName != "")
        {
            displayName_Text.text = displayName;
        }
        //if (vAxis != 0 || hAxis != 0)
        //{
        //    _rigidbody.velocity = (transform.forward * hAxis);
        //    transform.Rotate(transform.up * vAxis);
        //    hAxis = 0;
        //    vAxis = 0;

        //}
    }
}
