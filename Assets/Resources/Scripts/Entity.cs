using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health;
    protected float damage;
    public void takeDamage(float _damage)
    {
        health -= _damage;
        if(health <= 0)
        {
            die();
        }
    }
    public void die(){
        Destroy(gameObject);
    }
}
