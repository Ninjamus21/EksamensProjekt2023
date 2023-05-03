using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Entity : MonoBehaviour
{
    public float health;
    protected float damage;
    protected void TakeDamage(float _damage)
    {
        health -= _damage;
        if(health <= 0 && gameObject.tag != "Player")
        {
            die();
        } else if (health <= 0 && gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void die(){
        Destroy(gameObject);
    }
}
