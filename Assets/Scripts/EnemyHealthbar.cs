using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthbar : MonoBehaviour
{
    public Image healthbarForeground;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarForeground.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
