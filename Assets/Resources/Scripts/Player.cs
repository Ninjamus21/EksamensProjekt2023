using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Entity
{
    public Rigidbody body;
    public Camera cam;
    public float speed = 5.0f;
    public float ResetYThredhold = -0.5f;
    float moveLimiter = 0.7f;
    // speed up variables
    public bool speedup = false;
    public float buff = 1.0f;
    public float timeSinceSpeedUp = 1.0f;
    public float speedUpDuration = 10.0f;
    public float speedBoostAmount = 2.0f;
    Vector3 movement;
    Vector3 mousePos;

    void Update()
    {
        move();
        moisePos();
        Duration();
        SpeedUp();
    }
    void FixedUpdate()
    {
        look();
        overground();
        transformMove();
    }
     void OnCollisionEnter(Collision buff)
    {
        // if the player collides with a speed up power up
        if (buff.gameObject.tag == "SpeedUp")
        {
            speedup = true;
            SpeedUp();
        }
    }
    void move()
    {
        // Get horizontal and vertical input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Limit diagonal movement speed stops speed gains when pressing two keys.
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        // Set movement vector based on input, its makes the y axis 0 so the player doesn't move up or down
        movement = new Vector3(horizontal, 0f, vertical);
    }
    void moisePos()
    {
        // Get mouse position in world space
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        float distance;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(mouseRay, out distance))
        {
            mousePos = mouseRay.GetPoint(distance);
        }
    }


    void transformMove()
    {
        // Move the rigidbody based on movement vector and run speed
        body.MovePosition(body.position + movement * speed * buff * Time.fixedDeltaTime);
    }
    void look()
    {
        // Rotate the rigidbody to face the mouse position
        Vector3 lookDir = mousePos - body.position;
        lookDir.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        body.MoveRotation(rotation);
    }
    void overground()
    {
        // stop the player from going under the ground
        if (transform.position.y < ResetYThredhold)
        {
            body.velocity = Vector3.zero;
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }
    public void SpeedUp()
    {
        // Speed up the player
        if (speedup == true)
        {
            buff = speedBoostAmount;
            
        }
        else if (speedup == false)
        {
            buff = 1.0f;
        }
    }
    public void Duration()
    {
        // stop the player from being sped up forever
        while (speedup == true)
        {
            timeSinceSpeedUp += Time.deltaTime;
        } 

        if (timeSinceSpeedUp >= speedUpDuration)
        {
            speedup = false;
            timeSinceSpeedUp = 0.0f;
            
        }
        }
    } 

