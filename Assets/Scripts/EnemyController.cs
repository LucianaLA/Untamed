using System.Collections;
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

    public CombatController combatController;
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
        animator.SetBool("isAttacking", false);
        animator.SetBool("isChasing", true);
        
    }

    //enemy attack player function
    void Attack()
    {
        animator.SetBool("isAttacking", true);
    }

    
    //enemy on patrol function
    void Patrol()
    {
        if (!enableWalk) SetNewDest();
        if (enableWalk) {
            enemy.SetDestination(newDestination);
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
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

    IEnumerator DeathAnimation(GameObject Enemy){
        Debug.Log("code runs coroutine.");
        yield return new WaitForSeconds(2f);
        DropItem(Enemy);
        Enemy.gameObject.SetActive(false);
        Debug.Log("code runs death.");
    }

    public void DropItem(GameObject Enemy){
        int i = Random.Range(0,3);
        //drop item
        GameObject droppedItem = Instantiate(combatController.ItemDrop[i], new Vector3(Enemy.transform.position.x + Random.Range(-1.0f, 1.0f),
                                                    transform.position.y, Enemy.transform.position.z + Random.Range(-1.0f, 1.0f)),
                                                    Enemy.transform.rotation);
        Debug.Log(droppedItem+" item has been dropped.");
        
        droppedItem.SetActive(true);
    }
    public void EnemyDeath(GameObject Enemy){
        if (enemy_health <= 0){
            Debug.Log("Enemy died: "+ Enemy);
            Animator anim = Enemy.GetComponent<Animator>();
            anim.SetTrigger("ghostDeath");
            StartCoroutine(DeathAnimation(Enemy));
        }
    }
}
