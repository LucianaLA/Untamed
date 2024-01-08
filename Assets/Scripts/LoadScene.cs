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
            if (scene.name == "NS-Test")
            {
                SceneManager.LoadSceneAsync("EndCutscene");
            }
            
        }
    }
}