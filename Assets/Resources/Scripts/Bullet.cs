using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 bulletDirection;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Ranger")
        {
            //Yes, do nothing
        } 
        else if (collision.gameObject.tag == "Bullet")
        {
            //Yes, do nothing
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Vector3 normal = collision.contacts[0].normal;
            bulletDirection = Vector3.Reflect(bulletDirection, normal);
        }
        else
        {
            print("Collision" + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}    