using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Highlander : Enemy
{
    public float swingSpeed = 2.5f; // Speed of the sword swing
    private DateTime lastCallTime; // make sure the sword cant hit twice in a row
    public GameObject sword; // Reference to the sword GameObject
    public GameObject InstantiantedSword;
    public Transform player; // Reference to the player GameObject
    private Vector3 targetPosition; // The position that the highlander is moving towards
    public float swordDistance = 2f;
    private NavMeshAgent navAgent;
    public float fixedYPosition = 0.5f;
    public ParticleSystem particleEffectDie;
    public ParticleSystem particleEffectHit;


    void Start()
    {
        InstantiantedSword = Instantiate(sword);
        targetPosition = transform.position;

        // Set the position of the sword to be a certain distance away from the highlander
        InstantiantedSword.transform.position = transform.position + transform.forward * swordDistance;

        navAgent = GetComponent<NavMeshAgent>();
        health = 3;

        //Find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Attack();
        Move();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Shield")
        {
            // ensure that the shield cant hit the Highlander multiple times, and make particle effect ^^
            Cooldown();
        }
        else if (other.gameObject.tag == "2x bullet")
        {
            // take damage and make particle effect, this is legal since the highlander cant be hit multiple times by the 2x bullet
            TakeDamage(2);
            Instantiate(particleEffectHit, transform.position, Quaternion.identity);
        }
        else if (other.gameObject.tag == "Counter Bullet")
        {
            // take damage and make particle effect
            TakeDamage(1);
            Instantiate(particleEffectHit, transform.position, Quaternion.identity);
        }

    }
    public override void Attack()
    {
        // Rotate the sword around the highlander at a constant speed
        InstantiantedSword.transform.RotateAround(transform.position, Vector3.up, swingSpeed * Time.deltaTime);

        // Set the position of the sword to be a certain distance away from the highlander
        InstantiantedSword.transform.position = transform.position + transform.forward * swordDistance;

        // Ensure that the sword is always facing away from the highlander
        InstantiantedSword.transform.LookAt(transform.position);
        InstantiantedSword.transform.Rotate(new Vector3(0, 180, 0));
    }
    //make sure the sword cant hit the player multiple times
    public override void Cooldown()
    {
        TimeSpan timeSinceLastCall = DateTime.Now - lastCallTime;
        if (timeSinceLastCall.TotalSeconds < 0.5)
        {
            // Script has been called too soon, exit the method
            return;
        }
        // Script logic
        TakeDamage(1);
        Instantiate(particleEffectHit, transform.position, Quaternion.identity);

        // Update the last call time to the current time
        lastCallTime = DateTime.Now;

    }

    public override void Move()
    {
        if (navAgent != null && navAgent.isActiveAndEnabled)
        {
            Vector3 targetPosition = new Vector3(player.position.x, fixedYPosition, player.position.z);
            navAgent.SetDestination(targetPosition);
        }
    }
    public override void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Instantiate(particleEffectDie, transform.position, Quaternion.identity);
            // destroy the sword when the highlander dies
            Destroy(InstantiantedSword);
            die();
        }
    }
    public override void track()
    {
        throw new System.NotImplementedException();
    }
}