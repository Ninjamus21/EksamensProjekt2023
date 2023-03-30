using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    /*
    
    public float offset = 1f;
    public float rotationSpeed = 10f;
    public float followSpeed = 5f;
    private Vector3 targetPosition;


    void Start()
    {
    targetPosition = transform.position;
    }

    void Update()
    {
        lookAtEnemy();
        tet();
        transformMoveSword();

    }

    
    void DistanceFromEnemy()
    {
        Vector3 orbitPosition = new Vector3(
             TargetEnemy.transform.position.x + (offset * Mathf.Cos(_angle)),
             TargetEnemy.transform.position.y,
             TargetEnemy.transform.position.y + (offset * Mathf.Sin(_angle)));
        // Move the object to the orbit position
        transform.position = orbitPosition;

        // Increase the angle based on the speed
        _angle += rotationSpeed * Time.deltaTime;
    }
    
    void tet(){
    Vector3 targetPositionNoY = new Vector3(targetPosition.x, TargetEnemy.position.y, targetPosition.z);
            targetPosition = TargetEnemy.position + (targetPositionNoY - TargetEnemy.position).normalized * offset;

            // Rotate the shield to face the player and the mouse position at the same time (the shield will always be perpendicular to the ground)
            Vector3 lookDirection = transform.position - TargetEnemy.position;
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
        }
    public void lookAtEnemy()
    {
        transform.LookAt(TargetEnemy.transform);
    }
   void transformMoveSword()
   {
         transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
   }
   */
}