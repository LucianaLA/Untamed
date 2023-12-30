using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatController : MonoBehaviour
{
    public GameObject ShortSwordPrefab;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    public bool isAttacking = false;
    public bool canSpawn = true; //checks if new enemy drop can be spawned

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if(canAttack){
                NormalAttack();
            }
        }
    }

    public void NormalAttack(){
        isAttacking = true;
        // cannot attack while attacking
        canAttack = false;

        canSpawn = false;
        // adding animations 
        Animator anim = ShortSwordPrefab.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
        
    }

    IEnumerator ResetAttackCooldown(){
        StartCoroutine(ResetAttackBool());
        StartCoroutine(ResetEnemyDropCD());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetEnemyDropCD(){
        yield return new WaitForSeconds(attackCooldown * 1.5f);
        canSpawn=true;
    }

    IEnumerator ResetAttackBool(){
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
