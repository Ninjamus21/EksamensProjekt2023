using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

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
        else if (collision.gameObject.tag == "Shield")
        {
            //Yes, do nothing
        }
        else
        {
            print("Collision" + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}



