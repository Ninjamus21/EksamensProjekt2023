using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShieldRotate : Player {
    public Transform target;
    public float radius;
    private Transform pivot;
  void Start()
    {
        pivot = target.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }
    void Update () {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(target.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;
 
        pivot.position = target.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

    }
}




