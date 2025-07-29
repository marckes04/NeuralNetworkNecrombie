using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestriction : MonoBehaviour
{

    public float maxZ = -345f;
    public float minZ = -388f;
    public float minX = -176.6f;
    public float maxX = 168.8f;

    public GameObject player;

    private void Update()
    {
        Vector3 temp = player.transform.position;// Variable to stablish the current position


        if (temp.z > maxZ) // command to check if the player goes beyond maximum allowed place in Y axis
            temp.z = maxZ; // Maximum stablished position in Y axis
        if (temp.z < minZ) // command to check if the player goes beyond maximum allowed place in Y axis
            temp.z = minZ;
        if (temp.x > maxX) // command to check if the player goes beyond maximum allowed place in Y axis
            temp.x = maxX; // Maximum stablished position in Y axis
        if (temp.x < minX) // command to check if the player goes beyond maximum allowed place in Y axis
            temp.x = minX;


        player.transform.position = temp;//Final Position
    }
}