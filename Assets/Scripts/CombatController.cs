using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatController : MonoBehaviour
{
    public GameObject ShortSwordPrefab;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    public bool isAttacking = false;

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
        // adding animations 
        Animator anim = ShortSwordPrefab.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
        
    }

    IEnumerator ResetAttackCooldown(){
        StartCoroutine(ResetAttackCooldown());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool(){
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
