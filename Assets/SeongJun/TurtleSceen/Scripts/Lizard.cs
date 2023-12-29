using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public float Speed;
    public float UpSpeed;
    private Rigidbody TurtleRigidbody;

    void Start()
    {
        TurtleRigidbody = GetComponent<Rigidbody>();
        TurtleRigidbody.AddForce(0, 0, Speed);
    }

    public void Update()
    {
        TurtleRigidbody.AddForce(0, 0, UpSpeed);
    }
}
