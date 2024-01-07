using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    public FPSController FPSController;
    public GameObject settings;
    public GameObject tutorialPanel;
    public GameObject tutorial1;
    public GameObject tutorial2;

    // Start is called before the first frame update
    void Start()
    {
        ShowTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialPanel.activeInHierarchy)
        {
            FPSController.enableMove = false;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //open settings panel on pause
    public void onPlay()
    {
        Time.timeScale = 1f;
        settings.SetActive(false);
        FPSController.enableMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    //tutorial manager
    public void onClose()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        FPSController.enableMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void ShowTutorialOne()
    {
        tutorial1.SetActive(true);
        tutorial2.SetActive(false);
    }

    public void ShowTutorialTwo()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
    }

}
