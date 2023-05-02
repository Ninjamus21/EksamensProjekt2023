using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Highlander : Enemy
{
    public float swingSpeed = 5f; // Speed of the sword swing
    public GameObject sword; // Reference to the sword GameObject
    public GameObject InstantiantedSword;
    public Transform player; // Reference to the player GameObject
    private bool isSwinging = false; // Flag for tracking whether the highlander is currently swinging the sword
    private Vector3 targetPosition; // The position that the highlander is moving towards
    public float swordDistance = 2f;
    private NavMeshAgent navAgent;
    public float fixedYPosition = 0.5f;

    void Start()
    {
        InstantiantedSword = Instantiate(sword);
        targetPosition = transform.position;
        // Set the position of the sword to be a certain distance away from the highlander
        InstantiantedSword.transform.position = transform.position + transform.forward * swordDistance;
        
        navAgent = GetComponent<NavMeshAgent>();
        health = 3;
        damage = 1;

        //Find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        InstantiantedSword.transform.RotateAround(transform.position, Vector3.up, swingSpeed * Time.deltaTime);
        Attack();
    }
    public override void Attack()
    {
        // Rotate the sword around the highlander at a constant speed
        InstantiantedSword.transform.RotateAround(transform.position, Vector3.up, swingSpeed * Time.deltaTime);

        // Ensure that the sword is always facing away from the highlander
        InstantiantedSword.transform.LookAt(transform.position);
        InstantiantedSword.transform.Rotate(new Vector3(0, 180, 0));
    }
    public override void Cooldown()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public override void track()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
}