using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light day;
    public Light sunset;
    public Light night;
    // Set to 15 minutes for the entire cycle.
    public float dayDuration = 900.0f; // 15 minutes * 60 seconds

    private float timer = 0.0f;

    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;
        
        // Calculate the cycle progress as a percentage
        float cycleProgress = timer / dayDuration;
        
        // If the cycleProgress exceeds 1, a full cycle has passed, so reset the timer
        if (cycleProgress > 1) 
        {
            timer = 0.0f; // Reset the cycle 
        }

        // Daytime for the first 5 minutes
        day.enabled = cycleProgress <= (5f / 15f);
        // Sunset for the next 5 minutes
        sunset.enabled = cycleProgress > (5f / 15f) && cycleProgress <= (10f / 15f);
        // Nighttime for the last 5 minutes
        night.enabled = cycleProgress > (10f / 15f);
    }
}
