using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject oldObject; // reference to the old object
    public GameObject newObject; // reference to the new object
    public float velocityMulitplier = 2.0f;
    public GameObject player;
    public float buff = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ranger" || collision.gameObject.tag == "bullet")
        {
            //Yes, do nothing
        }
        else if (collision.gameObject.tag == "Shield")
        {
            ChangeObject();
        }
        else if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Counter Bullet")
        {

        }
        else
        {
            print("Collision" + collision.gameObject.name);
            Destroy(gameObject);
        }
    }

    void ChangeObject()
    {
        if (gameObject.tag == "Bullet")
        {

            // get the velocity of the old object
            Vector3 velocity = oldObject.GetComponent<Rigidbody>().velocity;

            // create a new instance of the new object
            GameObject newGameObject = Instantiate(newObject, oldObject.transform.position, oldObject.transform.rotation);
            // buff conditions for the bullet damage
            if (player.GetComponent<Player>().IsBuffedDamage) newGameObject.tag = "2x bullet";

            // buff conditions for the bullet speed recoil
            if (player.GetComponent<Player>().IsBuffedRecoil)
            { 
            velocityMulitplier *= buff;
            } else {
            velocityMulitplier = 2.0f;
            }
            // set the velocity of the new object to match the old object
            newGameObject.GetComponent<Rigidbody>().velocity = velocity * velocityMulitplier;

            // destroy the old object
            Destroy(oldObject);
        }
    }
}
