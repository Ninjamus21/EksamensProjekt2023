using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject targetObject;
    private RaycastHit hit;
    private float distance = 100.0f;
    private LayerMask targetLayerMask;

    // Start is called before the first frame update
    void Start()
    {
    targetLayerMask = LayerMask.GetMask("TargetObject");
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
           //Yes, do nothing
        }
        else
        {
            print("Collision" + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
    public void ray(){
        Debug.Log("Raycasting...");
       if (Physics.Raycast(transform.position, targetObject.transform.position - transform.position, out hit, distance, targetLayerMask))
        {
            Debug.DrawRay(transform.position, hit.point, Color.red);
            Debug.Log("Hit targetObject!");
        }
        }
    }
      