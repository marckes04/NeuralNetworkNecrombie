using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.instance.PlayMusic(audioSource);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
