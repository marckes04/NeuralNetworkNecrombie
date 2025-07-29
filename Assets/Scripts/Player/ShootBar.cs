using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBar : MonoBehaviour
{
    public static ShootBar instance;

    public Slider shoot;


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        shoot.minValue = 0;
        shoot.maxValue = 10;

        shoot.value = 10;
    }


    public void ShootUse()
    {
        shoot.value -= 2;

        if(shoot.value <= 0)
        {
            PlayerShooting.canShoot = false;
        }

    }


    public void RechargeMagic()
    {
        shoot.value += 4;
        if(shoot.value > shoot.maxValue) 
        {
            shoot.value = shoot.maxValue;        
        }


    }


}
