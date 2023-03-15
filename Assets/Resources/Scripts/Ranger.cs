using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranger : Enemy
{

    public Transform player;
    public GameObject bulletPrefab;
    public float fireRate = 10.0f;
    public float bulletSpeed = 10.0f;
    public float fixedYPosition = 0.5f;
    private float timeSinceLastFire = 0.0f;
    private NavMeshAgent navAgent;



    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Cooldown();
        Move();
    }
    void FixedUpdate()
    {
        track();
    }

    public override void Attack()
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 5.0f);
    }

    public override void track()
    {
        transform.LookAt(player);
    }
    public override void Move()
    {
        if (navAgent != null && navAgent.isActiveAndEnabled)
        {
            Vector3 targetPosition = new Vector3(player.position.x, fixedYPosition, player.position.z);
            navAgent.SetDestination(targetPosition);
        }
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float _damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Cooldown()
    {
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= 1.0f / fireRate + Random.Range(0.0f, 1.0f))
        {
            Attack();
            timeSinceLastFire = 0.0f;
        }
    }

}
