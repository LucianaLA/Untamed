using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool is_enabled = false;
    public float base_attack;
    public GameObject weaponGameObject;

    public int enhancementLevel = 1;

    public void Start()
    {
        // is_enabled = false;
        Default();
    }

    public void Update()
    {

    }

    public void Default()
    {

        // Adjust is_enabled based on specific conditions
        if (weaponGameObject.name == "Short Sword")
        {
            is_enabled = true;
            base_attack = 8;
            Debug.Log("sword enabled");
        }

        if(weaponGameObject.name == "Sword")
        {
            base_attack = 10;
        }
        if(weaponGameObject.name == "Battle Axe")
        {
            base_attack = 15;
        }
        if(weaponGameObject.name == "Throwing Axe")
        {
            base_attack = 5;
        }
    }
}
