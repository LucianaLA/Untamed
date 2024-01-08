using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{

    public FPSController FPSController;
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
        //get current scene
        Scene scene = SceneManager.GetActiveScene();

        //win condition for level 1
        if (scene.name == "Level 1")
        {
            ShowTutorial();
        }
        
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

    //default difficulty
    public void UpdateToDifficulty1()
    {
        GetEnemies(1);
        // weapon attack
        GetWeaponObj().Default();
        //player health
        basicNeeds.hunger_max = 300;
        basicNeeds.health_max = 300;

    }
    public void UpdateToDifficulty2()
    {
        GetEnemies(2);
        // weapon attack
        GetWeapons(3);
        //player health
        basicNeeds.hunger_max = 200;
        basicNeeds.health_max = 200;
    }
    public void UpdateToDifficulty3()
    {
        GetEnemies(3);
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
            float max_health = enemyController.enemy_maxhealth;
            float current_health = enemyController.enemy_health;
            if (health == 3){
                if (max_health == 200){
                    enemyController.enemy_health = 3 * current_health / 2;
                }
                else if (max_health == 100){enemyController.enemy_health = 3 * current_health;}
                enemyController.enemy_maxhealth = 300;
            } else if (health == 1){
                if (max_health == 200){
                    enemyController.enemy_health = current_health/2;
                }
                else if (max_health == 300) {enemyController.enemy_health = current_health/3;}
                enemyController.enemy_maxhealth = 100;
            } else if (health == 2){
                if (max_health == 300){
                    enemyController.enemy_health = 2*current_health/3;
                }
                else if (max_health == 100){enemyController.enemy_health = current_health*2;}
                enemyController.enemy_maxhealth = 200;
            } else if (health * 10 == max_health){
                Debug.Log("stop");
            }

            Debug.Log("enemy health: " + enemyController.enemy_health);
        }
    }

}
