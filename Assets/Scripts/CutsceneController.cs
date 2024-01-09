using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{

    public GameObject neighbour;
    //cutscenes
    public GameObject scene1;
    public GameObject scene2;

    //dialogues
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;

    [SerializeField]
    AudioSource audio;


    void Update()
    {
        if (neighbour.activeSelf)
        {
            audio.Stop();
        }
    }

    //change to second scene
    public void SecondScene()
    {
        scene2.SetActive(true);
        dialogue2.SetActive(true);
        GameObject.Find("Selection1").SetActive(false);
    }

    //change to third scene and add NPC character
    public void ThirdScene()
    {
        neighbour.SetActive(true);
        dialogue3.SetActive(true);
        GameObject.Find("Selection2").SetActive(false);
    }

    //start level
    public void StartLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }

    //back to menu
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
