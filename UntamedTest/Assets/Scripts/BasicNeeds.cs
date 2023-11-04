using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicNeeds : MonoBehaviour
{
    
    public GameObject loseHealth_popup;
    public GameObject loseHunger_popup;
    public Image hunger_bar_foreground;
    public Image health_bar_foreground;
    public GameObject background;
    public static float hunger_remaining;
    public static float health_remaining;
    public float hunger_max = 60.0f;
    public float health_max = 60.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        hunger_remaining = hunger_max;
        health_remaining = health_max;
    }

    void Update()
    {
        hunger_remaining -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hunger_remaining > 0)
        {
            hunger_bar_foreground.fillAmount = hunger_remaining / hunger_max;
            health_bar_foreground.fillAmount = health_remaining / hunger_max;
        }
        else{
            loseHunger_popup.SetActive(true);
            health_bar_foreground.gameObject.SetActive(false);
            background.SetActive(true);
        }

        if(health_remaining < 0)
        {
            loseHealth_popup.SetActive(true);
            hunger_bar_foreground.gameObject.SetActive(false);
            background.SetActive(true);
        }
    }
}
