using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Entity
{
    // private float health;
    // private float damage;
    // private float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack();
    public abstract void track();
    public abstract void Move();
    public abstract void Die();
    public abstract void Spawn();
    public abstract void TakeDamage(float _damage);
    public abstract void Cooldown();
}
