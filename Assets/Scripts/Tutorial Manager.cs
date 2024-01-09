using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorials;  // The parent tutorials GameObject
    public GameObject placeholderBackgroundTutorial;
    public GameObject combatTutorial;
    public GameObject combatTutorial2;

    private bool tutorial2Active = false;

    void Start()
    {
        ActivateTutorials();
    }

    void Update()
    {
        if (Input.anyKeyDown && tutorial2Active)
        {
            // When any key is pressed and tutorials are active, switch tutorials
            DeactivateTutorials();
        }
        if (Input.anyKeyDown){
            ActivateTutorials2();
        }
    }

    void ActivateTutorials()
    {
        tutorials.SetActive(true);
        placeholderBackgroundTutorial.SetActive(true);
        combatTutorial.SetActive(true);
        combatTutorial2.SetActive(false);
        tutorial2Active = false;
    }

    void ActivateTutorials2()
    {
        combatTutorial.SetActive(false);
            combatTutorial2.SetActive(true);
            tutorial2Active = true;
    }

    void DeactivateTutorials()
    {
        tutorials.SetActive(false);
        tutorial2Active = false;
    }
}
