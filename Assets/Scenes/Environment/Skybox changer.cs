using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AdvancedSkyboxChanger : MonoBehaviour
{
    public Material daySkybox;
    public Material sunsetSkybox;
    public Material nightSkybox;
    public Material volcanoSkybox; // Specific skybox near the volcano

    private float currentTimeOfDay = 0f; // Track the current time
    private const float dayDuration = 300f; // 5 minutes for day
    private const float sunsetDuration = 300f; // 5 minutes for sunset
    private const float nightDuration = 300f; // 5 minutes for night


void Update()
{
    // Increment the time
    currentTimeOfDay += Time.deltaTime;

    // Calculate total cycle time
    float totalCycleTime = dayDuration + sunsetDuration + nightDuration;

    // Reset the cycle if necessary
    if (currentTimeOfDay >= totalCycleTime)
    {
        currentTimeOfDay -= totalCycleTime; // or just set to 0
    }

    // Determine which part of the day it is
    if(currentTimeOfDay < dayDuration) // Daytime
    {
        RenderSettings.skybox = daySkybox;
    }
    else if(currentTimeOfDay < dayDuration + sunsetDuration) // Sunset
    {
        RenderSettings.skybox = sunsetSkybox;
    }
    else // Nighttime
    {
        RenderSettings.skybox = nightSkybox;
    }

    DynamicGI.UpdateEnvironment(); // Update the lighting
}

    // Assuming your player enters a collider around the volcano
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.skybox = volcanoSkybox;
            DynamicGI.UpdateEnvironment();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player leaves the volcano, revert to the time-appropriate skybox
        if (other.CompareTag("Player"))
        {
            // This will set the skybox to whichever is appropriate for the current time of day
            // based on the logic in Update()
            Update();
        }
    }
}