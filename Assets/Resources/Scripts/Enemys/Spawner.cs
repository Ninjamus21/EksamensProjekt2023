using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Spawner : Enemy
{
    private DateTime lastCallTime;
    private DateTime lastCallTime2;
    public GameObject particleEffectHit;
    public GameObject particleEffectDie;
    public GameObject Spawnling;
    public GameObject InstiatedSpawnling;
    public Transform player;
    private NavMeshAgent navAgent;
    public float fixedYPosition = 0.7f;
    private Vector3 targetPosition;
    public float spawnling_distance = 2.0f;
    private List<GameObject> activeSpawnlings = new List<GameObject>();
    public int maxActive = 100;
    public bool SpawnerisAlive = false;
    
    void Start()
    {
        targetPosition = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        health = 2;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnerisAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
    void FixedUpdate()
    {
        track();
    }

    void OnCollisionEnter (Collision col){
        if(col.gameObject.tag == "Shield"){
            Cooldown();
        }
        if (col.gameObject.tag == "Counter Bullet")
        {
            TakeDamage(1);
        }
        if (col.gameObject.tag == "2x bullet")
        {
            TakeDamage(2);
        }
    }
    public override void Attack()
    {
        if (activeSpawnlings.Count < maxActive){
    TimeSpan timeSinceLastCall2 = DateTime.Now - lastCallTime2;
    if (timeSinceLastCall2.TotalSeconds < 1.5)
    {
        // Script has been called too soon, exit the method
        return;
    }
    // Script logic

    Spawnling.transform.position = transform.position + transform.right * spawnling_distance;
    InstiatedSpawnling = Instantiate(Spawnling);
    activeSpawnlings.Add(Spawnling);

    Spawnling.transform.position = transform.position + transform.right *- spawnling_distance;
    InstiatedSpawnling = Instantiate(Spawnling);
    activeSpawnlings.Add(Spawnling);

    // Update the last call time to the current time
    lastCallTime2 = DateTime.Now;
    }
    }
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
            Destroy(InstiatedSpawnling);
            die();
        }
    }

    public override void track()
    {
        transform.LookAt(player);
    }

}
