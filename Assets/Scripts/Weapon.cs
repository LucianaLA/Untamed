using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool is_enabled = false;
    public float base_attack = 10;
    public GameObject weaponGameObject;

    public int enhancementLevel = 1;

    public void Start()
    {
        Default();
    }

    public void Update()
    {

    }
    private void Default()
    {
        // is_enabled = true; // Enable the weapon by default

        // Adjust is_enabled based on specific conditions
        if (weaponGameObject.name != "Short Sword")
        {
            is_enabled = true;
        }
    }
}
