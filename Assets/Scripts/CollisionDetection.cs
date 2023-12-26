using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public CombatController combatController;
    public GameObject EnemyDrop;

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy" && combatController.isAttacking){
            //animation
            //other.GetComponent<Animator>().SetTrigger("Hit");
            Debug.Log(other.name);
            Instantiate(EnemyDrop, new Vector3(other.transform.position.x, 
                                                transform.position.y, 
                                                other.transform.position.z), 
                                                other.transform.rotation);
            
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
