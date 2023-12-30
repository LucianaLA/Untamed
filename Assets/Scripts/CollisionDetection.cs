using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//This detects collision for combat system
public class CollisionDetection : MonoBehaviour
{
    public CombatController combatController;
    // public EnemyController enemyController;
    public GameObject EnemyDrop;
    private GameObject Enemy;

    // public GameObject Enemy;
    public FPSController fPSController;
    public static EnemyController EnemyControllerScript;

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

            // EnemyController.enemy_health -= fPSController.attack_power;
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
                EnemyControllerScript.EnemyDeath(Enemy);
                Debug.Log("Enemy health= "+EnemyControllerScript.enemy_health);
            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Enemy = GameObject.FindWithTag("Enemy");
        // Enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
