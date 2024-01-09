using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void ToggleOn()
    {
        AudioListener.volume = 1;
    }

    public void ToggleOff()
    {
       AudioListener.volume = 0;
    }
}