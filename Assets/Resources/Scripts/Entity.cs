using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private float health;
    private float damage;
    private float movespeed;
    void takeDamage(float _damage)
    {
        health -= _damage;
    }
}
