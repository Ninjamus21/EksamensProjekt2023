using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    // The particle effect to play when the box is destroyed.
    public ParticleSystem explosionEffect;

    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision buff){
    // destroy the box if it collides with a the player
    if (buff.gameObject.tag == "Player")
    {
        // Instantiate the explosion effect and destroy the particle system when its duration is up.
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    }
   
}

