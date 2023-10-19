using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
public class PlayerController : MonoBehaviour 
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    //checking if player is on ground to add drag
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.7f+0.2f, isGround);
        MyInput();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate(){
        MovePlayer();
    }


    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        //calc move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10F, ForceMode.Force);
    }

    //simple movement
    /*public Vector2 moveValue; 
    public float speed; 
    void OnMove(InputValue value) { 
        moveValue = value.Get<Vector2>(); 
        } 
        
    void FixedUpdate() { 
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y); 
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    } */

}