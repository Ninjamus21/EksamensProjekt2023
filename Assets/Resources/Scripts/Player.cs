using System.Threading.Tasks;
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
    public float buff = 1.0f;
    // recoil variables
    Bullet bullet = new Bullet();
    ShieldRotate shield = new ShieldRotate();
    Ranger ranger = new Ranger();
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
     void OnCollisionEnter(Collision buff)
    {
        // if the player collides with a speed up power up
        if (buff.gameObject.tag == "SpeedUp")
        {
            SpeedUp(2.0f);
            ShieldRotate(100f);
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((a) => SpeedUp(1.0f));
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((b) => ShieldRotate(15f));
        }
        // if the player collides with a recoil power up
        if (buff.gameObject.tag == "Recoil")
        {
            bullet.velocityMulitplier = 10.0f;
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((c) => bullet.velocityMulitplier = 2.0f);
        }
        if (buff.gameObject.tag == "Damage")
        {
            ranger.TakeDamage(2.0f);
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((d) => ranger.TakeDamage(1.0f));
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
    public void SpeedUp(float speedBoostAmount)
    {
        buff = speedBoostAmount;
    }
    public void ShieldRotate(float rotationSpeedBoostAmount)
    {
        shield.rotationSpeed = rotationSpeedBoostAmount;
    
    }
}
