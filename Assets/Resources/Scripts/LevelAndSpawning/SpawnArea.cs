using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    //You use the SerializeField attribute when you need your variable to be private but also want it to show up in the Editor.
    // make a private variable that can be seen in the editor essentially.
    [SerializeField] private Transform center; // The center point of the circle
    [SerializeField] private float radius; // The radius of the circle

    // Called when a new enemy is about to be spawned
    public bool CanSpawnEnemy(Vector3 position)
    {
        float distance = Vector3.Distance(center.position, position);
        return distance > radius; // If the distance between the spawn point and the center is greater than the radius, the enemy can spawn
    }

    // Draw the circle in the editor to help with visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center.position, radius);
    }
}

