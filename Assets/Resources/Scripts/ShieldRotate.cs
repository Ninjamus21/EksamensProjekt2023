using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotate : MonoBehaviour
{
   /*
    public Transform targetObject;

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        // Convert the mouse position to world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the angle between the vector from the target object to the mouse position and the x-axis
        float angle = Mathf.Atan2(mousePosWorld.z - targetObject.position.z, mousePosWorld.x - targetObject.position.x) * Mathf.Rad2Deg;

        // Set the rotation of the object
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

*/


 void Update()
{
   var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
   var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
   transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
}
}