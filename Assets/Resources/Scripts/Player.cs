using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Rigidbody body;
    public Camera cam;
    public float runSpeed = 20.0f;
    public float ResetYThredhold = -0.5f;

    float moveLimiter = 0.7f;
    Vector3 movement;
    Vector3 mousePos;

void Update()
    {
        move();
        moisePos();
    }
void FixedUpdate()
    {
        look();
        overground();
        transformMove();
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
    void moisePos(){
        // Get mouse position in world space
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        float distance;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(mouseRay, out distance))
        {
            mousePos = mouseRay.GetPoint(distance);
        }
    }
    
   
    void transformMove(){
        // Move the rigidbody based on movement vector and run speed
        body.MovePosition(body.position + movement * runSpeed * Time.fixedDeltaTime);
    } 
    void look()
    {
        // Rotate the rigidbody to face the mouse position
        Vector3 lookDir = mousePos - body.position;
        lookDir.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        body.MoveRotation(rotation);
    }
    void overground(){
        // stop the player from going under the ground
        if (transform.position.y < ResetYThredhold)
        {
            body.velocity = Vector3.zero;
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }

}
