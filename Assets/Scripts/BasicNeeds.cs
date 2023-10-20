using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicNeeds : MonoBehaviour
{
    
    public GameObject lose_popup;
    public Image hunger_bar_foreground;
    float hunger_remaining;
    public float hunger_max = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        hunger_remaining = hunger_max;
    }

    // Update is called once per frame
    void Update()
    {
        if(hunger_remaining > 0)
        {
            hunger_remaining -= Time.deltaTime;
            hunger_bar_foreground.fillAmount = hunger_remaining / hunger_max;
        }
        else{
            lose_popup.SetActive(true);

        }
    }
}
