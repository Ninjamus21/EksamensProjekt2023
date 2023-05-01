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

    // The audio clip to play when the box is destroyed.
    public AudioClip explosionSound;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
    // Check if the collider is the player.
    if (other.CompareTag("Player"))
    {
       Destroy(gameObject);
    }
}
}
