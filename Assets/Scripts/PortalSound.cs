using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{
    public AudioSource portalEntry;

    private void OnCollisionEnter(Collision collision)
    {
        portalEntry.Play();
    }
}
