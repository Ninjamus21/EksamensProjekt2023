using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void fixedUpdate()
    {
        void Move()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.right * horizontalInput * movespeed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * movespeed * Time.deltaTime);
        }
    }
    */
}
