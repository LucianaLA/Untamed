using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
//This detects collision for combat system
public class CollisionDetection : MonoBehaviour
{
    public CombatController combatController;
    public GameObject EnemyDrop;
    private GameObject Enemy;
    public FPSController fPSController;
    public EnemyController EnemyControllerScript;
    public EnemyHealthbar healthbar;

    float maxHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = EnemyControllerScript.enemy_health;
        healthbar.UpdateHealthBar(maxHealth, EnemyControllerScript.enemy_health); //set health bar
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && combatController.isAttacking)
        {
            //animation
            // other.GetComponent<Animator>().SetTrigger("Hit");
            // Debug.Log("hit"+ other.name);
            // Debug.Log("can spawn?" + combatController.canSpawn);
            if (combatController.canSpawn)
            {
                Instantiate(EnemyDrop, new Vector3(other.transform.position.x + Random.Range(-1.0f, 1.0f),
                                                    transform.position.y, other.transform.position.z + Random.Range(-1.0f, 1.0f)),
                                                    other.transform.rotation);
            }

            Enemy = other.gameObject;
            if (Enemy)
            {
                EnemyControllerScript = Enemy.GetComponent<EnemyController>();
                if (fPSController.energyFull)
                {
                    Debug.Log("strong attack used");
                    EnemyControllerScript.enemy_health -= fPSController.attack_power * 2; //stronger attack if energy full
                    fPSController.energyFull = false;
                    fPSController.attack_energy = 0; //reset energy after used up
                }
                else { EnemyControllerScript.enemy_health -= fPSController.attack_power; }

                Debug.Log("maxhealth: "+ maxHealth+", currentH: "+EnemyControllerScript.enemy_health);
                healthbar = EnemyControllerScript.healthbar;
                healthbar.UpdateHealthBar(maxHealth, EnemyControllerScript.enemy_health); //update health bar

                EnemyControllerScript.EnemyDeath(Enemy); //check if dead
                Debug.Log("Enemy health= "+EnemyControllerScript.enemy_health);
            }
        }
    }


}
