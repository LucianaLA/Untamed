using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthbar : MonoBehaviour
{
    public Image healthbarForeground;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarForeground.fillAmount = currentHealth / maxHealth;
    }
}
