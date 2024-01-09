using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CombatController : MonoBehaviour
{
    public Weapon[] Weapons;
    public GameObject[] ItemDrop;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    // private Weapon weapon;
    private GameObject activeWeapon;
    public bool isAttacking = false;
    public bool canSpawn = true; //checks if new enemy drop can be spawned

    // Start is called before the first frame update
    void Start()
    {
        PopulateWeaponArray();
        SetAllWeaponsActive(false);
        // Find the first active child and set it as the Weapon
        ShortSwordActive();
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

    void PopulateWeaponArray()
    {
        GameObject[] weaponObjects = GameObject.FindGameObjectsWithTag("Weapon");

        // Filter objects that have the Weapon component
        List<Weapon> validWeapons = new List<Weapon>();
        foreach (GameObject weaponObject in weaponObjects)
        {
            Weapon weaponComponent = weaponObject.GetComponent<Weapon>();
            if (weaponComponent != null)
            {
                validWeapons = validWeapons.OrderBy(weapon => weapon.name).ToList();
                validWeapons.Add(weaponComponent);
            }
        }

        // Convert the list to an array
        Weapons = validWeapons.ToArray();
    }

    void SetAllWeaponsActive(bool isActive)
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.weaponGameObject.SetActive(isActive);
        }
    }
void SetActiveWeapon(int activeIndex)
{
    if (activeIndex >= 0 && activeIndex < Weapons.Length)
    {
        Weapon selectedWeapon = Weapons[activeIndex];

        // Check if the selected weapon is enabled
        if (selectedWeapon.is_enabled)
        {
            activeWeapon = selectedWeapon.weaponGameObject;  // Set the active weapon
            SetAllWeaponsActive(false); // Deactivate all weapons
            activeWeapon.SetActive(true); // Activate the selected weapon
        }
    }
}

    public GameObject GetActiveWeapon()
    {
        return activeWeapon;
    }

    public void ShortSwordActive(){
        foreach (Weapon weapon in Weapons)
        {
            // Debug.Log(weapon.name);
            if (weapon.name == "Short Sword"){
                Debug.Log("runs");
                weapon.weaponGameObject.SetActive(true);
                activeWeapon = weapon.gameObject;
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
