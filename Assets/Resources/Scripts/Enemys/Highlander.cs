using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlander : Enemy
{
    public GameObject sword;
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Cooldown()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
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
    void Start()
    {
        Instantiate(sword);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
