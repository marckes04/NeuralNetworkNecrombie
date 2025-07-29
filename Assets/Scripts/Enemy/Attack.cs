using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    private GameObject player;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            DoAttack();
        }
    }

    private void DoAttack()
    {
        Destroy(player);
    }

}
