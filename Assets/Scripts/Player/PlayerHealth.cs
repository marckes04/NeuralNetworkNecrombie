
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealth: MonoBehaviour
{
    public static PlayerHealth instance;

    public Slider health;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health.minValue = 0;
        health.maxValue = 10;
        health.value = 10;
    }


    public void Damage()
    {
        health.value -= 2;

        if(health.value <= 0)
        {
          
          GameManager.instance.PlayerStaus(0);

        }

    }


    public void RestoreLife()
    {
        health.value = float.MaxValue;
    }
  
}
