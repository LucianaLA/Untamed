using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
///
    float mouseX;
    float mouseY = 100;

    float yRotation;
    float xRotation;

    public GameObject player;
///
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

        //RotationPlayer(mouseX, mouseY);

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = groundDrag; //should be 0
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    // trying to rotate player with camera/mouse
    private void RotationPlayer(float x, float y){
        xRotation += y;
        yRotation += x;
        transform.rotation = Quaternion.Euler(0, yRotation *100, 0);
        orientation.rotation = Quaternion.Euler(xRotation,  0, 0);
        transform.position = player.transform.position;
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //trying to move with mouse direction
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
    }

    private void MovePlayer(){
        //calc move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;// * mouseX; //mouseY;
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