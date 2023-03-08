using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    Rigidbody body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        // moving 
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        // rotate player to face mouse position on screen 
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        Debug.DrawRay(transform.position, Camera.main.WorldToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)), Color.blue);


    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
    }
}