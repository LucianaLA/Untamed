using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{

    public FPSController FPSController;
    public GameObject information;
    public GameObject settings;
    public GameObject tutorialPanel;
    public GameObject tutorial1;
    public GameObject tutorial2;

    private string enemyTag = "Enemy";
    private string weaponTag = "Weapon";
    [SerializeField] CombatController combatController;

    [SerializeField] Weapon weapon;

    [SerializeField] BasicNeeds basicNeeds;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (information.activeInHierarchy)
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

    //close information and show tutorial
    public void CloseInformation()
    {
        information.SetActive(false);
        ShowTutorial();
    }

    //tutorial manager
    public void CloseTutorial()
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

    //default difficulty
    public void UpdateToDifficulty1()
    {
        GetEnemies(100);
        // weapon attack
        GetWeaponObj().Default();
        //player health
        basicNeeds.hunger_max = 300;
        basicNeeds.health_max = 300;

    }
    public void UpdateToDifficulty2()
    {
        GetEnemies(200);
        // weapon attack
        GetWeapons(3);
        //player health
        basicNeeds.hunger_max = 200;
        basicNeeds.health_max = 200;
    }
    public void UpdateToDifficulty3()
    {
        GetEnemies(300);
        // weapon attack
        GetWeapons(2);
        //player health
        basicNeeds.hunger_max = 100;
        basicNeeds.health_max = 100;
    }

    private Weapon GetWeaponObj(){
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(weaponTag);
        foreach (GameObject weaponObject in weapons)
        {
            Weapon weaponController = weaponObject.GetComponent<Weapon>();
            //enemy health
            return weaponController;
        } return null;
    }
    private void GetWeapons(float atkChange)
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(weaponTag);
        foreach (GameObject weaponObject in weapons)
        {
            Weapon weaponController = weaponObject.GetComponent<Weapon>();
            //enemy health
            float currentatk = weaponController.base_attack;
            weaponController.base_attack -= (currentatk/atkChange);
            Debug.Log("weapon atk: " + weaponController.base_attack);
        }
    }
    private void GetEnemies(float health)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemyObject in enemies)
        {
            EnemyController enemyController = enemyObject.GetComponent<EnemyController>();
            //enemy health
            enemyController.enemy_health = health;
            Debug.Log("enemy health: " + enemyController.enemy_health);
        }
    }

}
