using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private NpcDialogue npcDialogue;
    private bool isClose;
    private void OnTriggerEnter(Collider other)
   
    {
        if (other.gameObject.CompareTag("NPC")) 
        {
            Debug.Log("npc detected");
            isClose = true;
            
        } 

     }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("npc out of range");
            isClose = false;

        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isClose)
        {
            Debug.Log("Input detected");
            npcDialogue.ShowUI();

        }
    }



}
