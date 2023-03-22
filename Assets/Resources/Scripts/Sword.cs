using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform TargetObject;
    public float spinSpeed = 5f;
    public float distanceToEnemy = 1f;
    public Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
      // Get the direction vector from the current object to the center object
        Vector3 direction = TargetObject.position - transform.position;
        direction.y = 0; // Set the y-component to zero so the object stays on the y-axis

        // Calculate the rotation to look at the center object while staying level on the y-axis
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the rotation gradually over time using Lerp for smoothness
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * spinSpeed);
    }
    public void swordRotation()
    {
        transform.RotateAround(TargetObject.position, Vector3.down, spinSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
    }
    public void stayAtEnemy()
    {
        Vector3 targetPosition = TargetObject.position;
        targetPosition.y = transform.position.y;
        transform.position = TargetObject.position - transform.forward * distanceToEnemy + offset;
    }
}
