using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBullet : MonoBehaviour
{
    public float time = 5.0f;
    public ParticleSystem particleEffectHIT;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, time);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ranger")
        {
            Instantiate(particleEffectHIT, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
