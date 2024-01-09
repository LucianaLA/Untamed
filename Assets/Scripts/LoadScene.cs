using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //get current scene
            Scene scene = SceneManager.GetActiveScene();

            //portal to level 2
            if (scene.name == "Level 1")
            {
                SceneManager.LoadSceneAsync("Level 2");
            }

            //portal to level 3
            if (scene.name == "Level 2")
            {
                SceneManager.LoadSceneAsync("Level 3");
            }

            //portal to end cutscene
            if (scene.name == "Level 3")
            {
                SceneManager.LoadSceneAsync("EndCutscene");
            }
        }
    }
}