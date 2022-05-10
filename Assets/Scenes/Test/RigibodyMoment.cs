using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigibodyMoment : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float Jumforce;

    // Start is called before the first frame update
    private void Awake()
    {
        PlayerCamera = Camera.main.transform;
        PlayerBody = GetComponent<Rigidbody>();
        Speed = 5f;
        Sensitivty = 3;
        Jumforce = 10;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayerCamera()
    {
        throw new NotImplementedException();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMouseInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * Jumforce, ForceMode.Impulse); 
        }

    }
}
