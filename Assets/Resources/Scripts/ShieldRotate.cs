using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotate : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer = 2f;
    public float rotationSpeed = 5f;
    private Vector3 targetPosition;
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

    void ShieldPosition()
    {
        // Get mouse position in world space
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
    }
 
}