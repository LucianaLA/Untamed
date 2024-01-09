using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource m; 
    public AudioSource se; 

    public void ToggleMusic(bool enabled)
    {
        m.mute = !enabled;
    }

    public void ToggleSoundEffects(bool enabled)
    {
        se.mute = !enabled;
    }
}