using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    public FPSController FPSController;
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //open settings panel on pause
    public void onPlay()
    {
        Time.timeScale = 1f;
        settings.SetActive(false);
        Debug.Log("Button is pressed");
        FPSController.enableMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

}
