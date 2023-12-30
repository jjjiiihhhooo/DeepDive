using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovemont : MonoBehaviour
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
        
    }

    public void OnMouseDown()
    {
        TurtleMovemont turtleMovemont = GetComponent<TurtleMovemont>();
        turtleMovemont.SpeedUpTurtle();
    }

    public void SpeedUpTurtle()
    {
        TurtleRigidbody.AddForce(0, 0, UpSpeed);
        Speed += UpSpeed;
    }

}
