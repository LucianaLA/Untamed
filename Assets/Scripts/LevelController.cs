using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    //portal
    public GameObject portal;
    public GameObject cat;
    public float portalDistance;
    public float catDistance;

    //win popup
    public GameObject winPopup;
    public bool enablePopup;

    //enemy kill counter
    public int counterKill;
    public TextMeshProUGUI counterText;
    //number of kills to win
    public int killCondition;

    //reference player camera movement during popup
    public FPSController FPSController;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        portalDistance = 5f;
        catDistance = 3f;
        enablePopup = false;
        //access player script to get player movement
        FPSController = GameObject.Find("Player").GetComponent<FPSController>();

        //get current scene
        Scene scene = SceneManager.GetActiveScene();

        //win condition for level 1
        if (scene.name == "Level 1")
        {
            killCondition = 1;
        }

        //win condition for level 2
        if (scene.name == "Level 2")
        {
            killCondition = 15;
        }

        //win condition for level 2
        if (scene.name == "Level 3")
        {
            killCondition = 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "Ghosts to kill: " + killCondition + "\nGhosts killed: " + counterKill;
    }

    void FixedUpdate()
    {
        if (enablePopup == false)
        {
            CheckWinCondition();
        }

    }

    //check if player met condition to win level and unlock portal
    void CheckWinCondition()
    {
        if (counterKill == killCondition)
        {
            enablePopup = true;
            ShowPopup();

            //get current scene
            Scene scene = SceneManager.GetActiveScene();

            //win condition for level 1
            if (scene.name != "Level 3")
            {
                SpawnPortal();
            }

            else
            {
                SpawnCat();
            }
        }
    }

    void ShowPopup()
    {
        Debug.Log("Player Won");
        Time.timeScale = 0f;
        winPopup.SetActive(true);
        //disable player movement during popup
        FPSController.enableMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    //close win popup
    public void OnClose()
    {
        Time.timeScale = 1f;
        winPopup.SetActive(false);
        FPSController.enableMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //spawn portal in front of player
    void SpawnPortal()
    {
        Instantiate(portal, (player.transform.forward * portalDistance) + player.transform.position, Quaternion.identity);
    }

    void SpawnCat()
    {
        Instantiate(cat, (player.transform.forward * catDistance) + player.transform.position, Quaternion.identity);
        SpawnPortal();
    }


    //restart level
    public void RestartLevel()
    {
        //get current scene
        Scene scene = SceneManager.GetActiveScene();

        //restart level 1
        if (scene.name == "Level 3")
        {
            SceneManager.LoadSceneAsync("Level 1");
        }

        //restart level 3
        if (scene.name == "Level 3")
        {
            SceneManager.LoadSceneAsync("Level 2");
        }

        //restart level 3
        if (scene.name == "Level 3")
        {
            SceneManager.LoadSceneAsync("Level 3");
        }

    }

    //go back to main menu

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
