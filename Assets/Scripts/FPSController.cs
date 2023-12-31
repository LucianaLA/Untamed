using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    //player camera view
    public Camera playerCamera;
    public float rotationSpeed = 2f;
    float rotationX = 0;
    public float rotationX_max = 45f;

    //player movement + jump speed
    public float walkSpeed = 6f;
    public float jumpForce = 7f;
    public float runSpeed = 12f;
    public float gravity = 10f;
    Vector3 moveDirection = Vector3.zero;

    public bool enableRunning;
    public bool enableMove = true;
    CharacterController characterController;
    //COMBAT SYSTEM 
    public CombatController combatController;

    public GameObject Combat;

    public float attack_power = 10f;
    public float attack_energy = 0;

    public float max_energy = 7;
    public bool energyFull = false;
    //player stats
    public float hungerCount;
    public float healthCount;


    //settings UI
    public GameObject settings;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        //player movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //player run (shift)
        enableRunning = Input.GetKey(KeyCode.LeftShift);
        float cursorXSpeed = enableMove ? (enableRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float cursorYSpeed = enableMove ? (enableRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * cursorXSpeed) + (right * cursorYSpeed);

        //player jump
        if (Input.GetButton("Jump") && enableMove && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        //camera view rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (enableMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationX = Mathf.Clamp(rotationX, -rotationX_max, rotationX_max);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        }
        GameOverCheck();
        EnergyManagement();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            triggerPause();
        }
    }

    //colliding with enemy
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BasicNeeds.health_remaining -= healthCount;
            Debug.Log("Damage taken");
        }
    }

    IEnumerator Respawn(Collider other, int time)
    {
        yield return new WaitForSeconds(time);
        other.gameObject.SetActive(true);
    }
    //colliding with pickups
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            BasicNeeds.hunger_remaining += hungerCount;
            Debug.Log("Hunger increased");
            //set respawn time
            StartCoroutine(Respawn(other, 25));
        }
        if (other.gameObject.tag == "EnemyDrop")
        {
            other.gameObject.SetActive(false);
            attack_energy += 1; //increase energy via picking up drops
            Debug.Log("Pickup enemy drop");
        }
    }

    public void EnergyManagement()
    {
        if (attack_energy >= max_energy)
        {
            energyFull = true; // when 5 or more drops picked up, enables stronger attack
        }
    }

    public void GameOverCheck()
    {
        if (BasicNeeds.win_check == true || BasicNeeds.is_dead == true)
        {
            enableMove = false;
            enableRunning = false;
            Combat.SetActive(false);
        }
    }

    //trigger pause and show settings
    void triggerPause()
    {
        enableMove = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        settings.SetActive(true);
    }

}