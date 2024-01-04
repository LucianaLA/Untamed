using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatController : MonoBehaviour
{
    public GameObject[] Weapons;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    private GameObject activeWeapon;
    public bool isAttacking = false;
    public bool canSpawn = true; //checks if new enemy drop can be spawned

    // Start is called before the first frame update
    void Start()
    {
        // Find the first active child and set it as the Weapon
        SetActiveWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                NormalAttack();
            }
        }

        // Check if key 1 is pressed and activate the first child
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(0);
        }
        // Check if key 2 is pressed and activate the second child
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(1);
        }
        // Check if key 3 is pressed and activate the third child
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveWeapon(3);
        }
    }

    void SetActiveWeapon(int activeIndex)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (i == activeIndex)
            {
                Weapons[i].SetActive(true);
                activeWeapon = Weapons[i];  // Set the active weapon
            }
            else
            {
                Weapons[i].SetActive(false);
            }
        }
    }

    public void NormalAttack()
    {
        isAttacking = true;
        // cannot attack while attacking
        canAttack = false;

        canSpawn = false;
        // adding animations 
        Animator anim = activeWeapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        StartCoroutine(ResetEnemyDropCD());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetEnemyDropCD()
    {
        yield return new WaitForSeconds(attackCooldown * 1.5f);
        canSpawn = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
