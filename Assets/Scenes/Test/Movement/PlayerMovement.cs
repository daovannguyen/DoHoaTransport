using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody RbBody;

    public float Speed { get; private set; }

    private void Awake()
    {
        Speed = 100;
        RbBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed;
        float inputY = Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed;
        
        RbBody.AddForce(Vector3.forward * inputX * Time.deltaTime * Speed);
    }
}
