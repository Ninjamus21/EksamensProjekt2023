using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotate : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer = 2f;
    public float rotationSpeed = 5f;
    private Vector3 targetPosition;
    public float buff = 3f;
    
    void start()
    {
        targetPosition = transform.position;
        
        
    }
    void Update()
    {
        ShieldPosition();
    }
    void FixedUpdate()
    {
        transformMoveShield();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SpeedUp")
        {
            SpeedShield();
        }
    }
    void ShieldPosition()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        // If the ray hits the ground plane, set the target position to the mouse position 
        if (groundPlane.Raycast(mouseray, out distance))
        {
            targetPosition = mouseray.GetPoint(distance);

            // Calulate the rotation needed to look at the target position, and move the shield towards it
            Vector3 targetPositionNoY = new Vector3(targetPosition.x, player.position.y, targetPosition.z);
            targetPosition = player.position + (targetPositionNoY - player.position).normalized * distanceFromPlayer;

            // Rotate the shield to face the player and the mouse position at the same time (the shield will always be perpendicular to the ground)
            Vector3 lookDirection = transform.position - player.position;
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
        }

    }
    void transformMoveShield()
    {
        // Move the shield towards the target position at a constant speed
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
    }

    public void SpeedShield()
    {
        if (player.GetComponent<Player>().IsBuffedSpeedShield == true)
        {
            rotationSpeed *= buff;
        } else {
            rotationSpeed = 15f;
        }
    }
}