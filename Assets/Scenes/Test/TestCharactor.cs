using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharactor : MonoBehaviour
{
    float speed = 200f;
    float rotationSpeed = 100f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        _rigidbody.velocity = (transform.forward * vAxis * speed * Time.deltaTime);
        transform.Rotate(transform.up * hAxis * rotationSpeed * Time.deltaTime);
    }
}
