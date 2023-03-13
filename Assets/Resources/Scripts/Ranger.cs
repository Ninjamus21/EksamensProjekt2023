using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Enemy
{

public Transform player;
public GameObject bulletPrefab;
public float fireRate = 1.0f;
public float bulletSpeed = 10.0f;
public float distanceToPlayer = 10.0f;
private float timeSinceLastFire = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Cooldown();
    }
    void FixedUpdate()
    {
        track();
        Attack();
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
        transform.position = player.position - transform.forward * distanceToPlayer;

    }
    public override void Move()
    {
        throw new System.NotImplementedException();
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
        if (timeSinceLastFire > 1.0f / fireRate) {
        Attack();
        timeSinceLastFire = 0.0f;
    }
}
}
