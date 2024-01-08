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
    public float distance;

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
        distance = 1.5f;
        enablePopup = false;
        //access player script to get player movement
        FPSController = GameObject.Find("Player").GetComponent<FPSController>();

        //get current scene
        Scene scene = SceneManager.GetActiveScene();

        //win condition for level 1
        if (scene.name == "Level 1 - Backup" || scene.name == "Level 1")
        {
            killCondition = 1;
        }

        //win condition for level 2
        if (scene.name == "Level 2")
        {
            killCondition = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "Ghosts to kill: " + killCondition +"\nGhosts killed: " + counterKill;
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
            SpawnPortal();
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
        Instantiate(portal, (player.transform.forward * distance) + player.transform.position, Quaternion.identity);
    }
}
