using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkyboxChanger : MonoBehaviour
{
    public Material day;
    public Material sunset;
    public Material night;

    private float currentTime = 0f; // Track the current time
    private const float dayDuration = 300f; // 5 minutes for day
    private const float sunsetDuration = 300f; // 5 minutes for sunset
    private const float nightDuration = 300f; // 5 minutes for night


void Update()
{
    // Increment the time
    currentTime += Time.deltaTime;

    // Calculate total cycle time
    float totalCycleTime = dayDuration + sunsetDuration + nightDuration;

    // Reset the cycle 
    if (currentTime >= totalCycleTime)
    {
        currentTime -= totalCycleTime; 
    }

    // Determine which part of the day it is
    if(currentTime < dayDuration) // Daytime
    {
        RenderSettings.skybox = day;
    }
    else if(currentTime < dayDuration + sunsetDuration) // Sunset
    {
        RenderSettings.skybox = sunset;
    }
    else // Nighttime
    {
        RenderSettings.skybox = night;
    }

    DynamicGI.UpdateEnvironment(); // Update the lighting
}



}