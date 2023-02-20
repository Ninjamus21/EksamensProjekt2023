using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotate : MonoBehaviour {
    public Transform target;
    public float speed = 5.0f;
 
    void Update () {
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.RotateAround(target.position, Vector3.up, horizontalInput * speed);
    }
}




