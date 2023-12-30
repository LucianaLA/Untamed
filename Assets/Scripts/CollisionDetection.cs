using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This detects collision for combat system
public class CollisionDetection : MonoBehaviour
{
    public CombatController combatController;
    public GameObject EnemyDrop;

    

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy" && combatController.isAttacking){
            //animation
            // other.GetComponent<Animator>().SetTrigger("Hit");
            // Debug.Log("hit"+ other.name);
            // Debug.Log("can spawn?" + combatController.canSpawn);
            if (combatController.canSpawn){
                Instantiate(EnemyDrop, new Vector3(other.transform.position.x+Random.Range(-1.0f, 1.0f), 
                                                    transform.position.y, other.transform.position.z+Random.Range(-1.0f, 1.0f)), 
                                                    other.transform.rotation);
            }
            //EnemyController.enemy_health -= attack_power;
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
