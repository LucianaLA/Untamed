using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthbar : MonoBehaviour
{
    public Image healthbarForeground;
    private float target = 1;
    public float reduceSpeed = 4;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthbarForeground.fillAmount = Mathf.MoveTowards(healthbarForeground.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
