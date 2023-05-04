using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnlings : MonoBehaviour
{
    public Transform target;
    public GameObject spawner;
    public float speed = 10f;
    public float rotateSpeed = 5f;
    public float drag = 2f;
    public float maxVelocity = 9f;
    private Vector3 velocity;

    
    void Start () 
    {
        // Set the target to the player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spawner = GameObject.FindGameObjectWithTag("Spawner");

    }
    void Update () 
    {
        NuStopperDet();
        movemento();
    }
    void OnCollisionEnter (Collision col){
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Shield" || col.gameObject.tag == "Counter Bullet" || col.gameObject.tag == "2x bullet" || col.gameObject.tag == "Bullet"){
            Destroy(gameObject);
        }
    }
    public void NuStopperDet()
    {
        /*
        if (spawner.GetComponent<Spawner>().SpawnerisAlive)
        {
            Destroy(gameObject);
        }
        */
        
    }
    public void movemento(){
        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        
        // Rotate towards the target
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        
        // Calculate the acceleration
        Vector3 acceleration = direction * speed - velocity * drag;
        
        // Update the velocity and clamp it to the maximum velocity
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        
        // Move the missile based on the velocity
        transform.position += velocity * Time.deltaTime;

        transform.LookAt(target);
    }
}