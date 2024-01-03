using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //enemy stats
    public float enemy_health = 100f;
    public EnemyHealthbar healthbar;
    GameObject player;
    NavMeshAgent enemy;
    Rigidbody rb;

    //assign layers for the ground and player
    [SerializeField] LayerMask groundLayer, playerLayer;

    //set new destination for patrol
    Vector3 newDestination;

    //
    public static bool enableWalk;
    [SerializeField] float range;

    //state change
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;


    //enemy animation
    Animator animator;

    void Start()
    {
        //get enemy and player object
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
      
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSight && !playerInAttackRange) Patrol();
        if (playerInSight && !playerInAttackRange) Chase();
        if (playerInSight && playerInAttackRange) Attack();
    }


    //enemy chase player function
    void Chase()
    {
        enemy.SetDestination(player.transform.position);
        animator.SetBool("isChasing", true);
        
    }

    //enemy attack player function
    void Attack()
    {
    }

    
    //enemy on patrol function
    void Patrol()
    {
        if (!enableWalk) SetNewDest();
        if (enableWalk) {
            enemy.SetDestination(newDestination);
            animator.SetBool("isChasing", false);
        }

        //set enemy walk to false if new destination is below range
        if (Vector3.Distance(transform.position, newDestination) < 10) enableWalk = false;
    }

    //set new destination for enemy to walk to
    void SetNewDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        newDestination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(newDestination, Vector3.down, groundLayer))
        {
            enableWalk = true;
        }
    }

    public void EnemyDeath(GameObject Enemy){
        if (enemy_health <= 0){
            Debug.Log("Enemy died: "+ Enemy);
            Enemy.gameObject.SetActive(false);
            // enemy_health = 100;
        }
    }
}
