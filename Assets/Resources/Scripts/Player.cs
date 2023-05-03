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
    private DateTime lastCallTime; // make sure the sword cant hit twice in a row
    public ParticleSystem particleEffectHit;
    // speed up variables
    public float buff = 1.0f;
    // recoil variables
    Vector3 movement;
    Vector3 mousePos;
    // material variables
    public Material[] materials;
    public Renderer rend;

    // damage variables
    public bool IsBuffedDamage = false;
    public bool IsBuffedSpeedShield = false;
    public bool IsBuffedRecoil = false;

    void Start()
    {
        health = 3;
        damage = 1;
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        move();
        moisePos();
        HpChange();
    }
    void FixedUpdate()
    {
        look();
        overground();
        transformMove();
    }
     void OnCollisionEnter(Collision PlayerCOL)
    {
        // if the player collides with a speed up power up
        if (PlayerCOL.gameObject.tag == "SpeedUp")
        {
            SpeedUp(2.0f);
            IsBuffedSpeedShield = true;
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((a) => SpeedUp(1.0f));
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((a) => IsBuffedSpeedShield = false);

        }
        if (PlayerCOL.gameObject.tag == "Damage")
        {
            IsBuffedDamage = true;
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((a) => IsBuffedDamage = false);
        }
        if (PlayerCOL.gameObject.tag == "Recoil")
        {
            IsBuffedRecoil = true;
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((a) => IsBuffedRecoil = false);
        }
        // damage system for the player
        if (PlayerCOL.gameObject.tag == "Bullet")
        {
            takeDamage(1);
            Instantiate(particleEffectHit, transform.position, Quaternion.identity);
        }
        if (PlayerCOL.gameObject.tag == "Sword")
        {
            Cooldown();
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
    public void Cooldown()
    {
    TimeSpan timeSinceLastCall = DateTime.Now - lastCallTime;
    if (timeSinceLastCall.TotalSeconds < 1.5)
    {
        // Script has been called too soon, exit the method
        return;
    }
    // Script logic
    takeDamage(1);
    Instantiate(particleEffectHit, transform.position, Quaternion.identity);

    // Update the last call time to the current time
    lastCallTime = DateTime.Now;

    }
    public void HpChange(){
        if (health == 3)
        {
            rend.sharedMaterial = materials[0];
        }
        if (health == 2)
        {
            rend.sharedMaterial = materials[1];
        }
        if (health == 1)
        {
            rend.sharedMaterial = materials[2];
        }
    }
}
