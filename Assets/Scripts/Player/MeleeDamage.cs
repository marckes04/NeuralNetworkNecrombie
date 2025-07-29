using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1f;
    public int damage = 30;
   
    public float destroyDelay = 2.5f;

    void Update()
    {
        // Detect any objects within the radius that are part of the specified layer
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);

        if (hits.Length > 0)
        {
            EnemyLife enemyLife = hits[0].GetComponent<EnemyLife>();
            if (enemyLife != null)
            {
                // Apply damage to the enemy
                enemyLife.TakeDamage(damage);

             


                // Deactivate the game object that caused the damage
                gameObject.SetActive(false);
            }
        }
    }
}
