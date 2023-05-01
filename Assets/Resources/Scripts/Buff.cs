using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    // The speed boost amount.
    public float speedBoostAmount = 2f;

    // The duration of the speed boost in seconds.
    public float speedBoostDuration = 5f;

    // The particle effect to play when the box is destroyed.
    public ParticleSystem explosionEffect;

    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision buff){
    // destroy the box if it collides with a the player or the shield
    if (buff.gameObject.tag == "Player" || buff.gameObject.tag == "Shield")
    {
        // Instantiate the explosion effect and destroy the particle system when its duration is up.
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    }
   
}

