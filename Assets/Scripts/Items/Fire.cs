using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireIntensity = 0.2f;
    public float firePerSecond = 3.0f;
    public float speedRandomness = 1.0f;
    public float minimumIntensity = 4.0f;

    private float time;
    private float startingIntensity;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
        startingIntensity = light.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        float flicker = Mathf.Sin(time * firePerSecond) * fireIntensity;
        light.intensity = Mathf.Max(startingIntensity + flicker, minimumIntensity);
    }
}
