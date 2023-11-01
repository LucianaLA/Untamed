using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine; 
using UnityEngine.InputSystem; 
public class PlayerController : MonoBehaviour 
{
//rotation
    public float horizontalSpeed = 2.0F;
     public float verticalSpeed = 2.0F;
     //.rotation
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
private Vector3 MousePositionViewport = Vector3.zero;
private Quaternion DesiredRotation = new Quaternion();
private float RotationSpeed = 15;
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
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
         float v = verticalSpeed * Input.GetAxis("Mouse Y");
         transform.Rotate(v, h, 0);

        //RotationPlayer();

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
    private void RotationPlayer(){

    if (Input.GetKey("a")) {
         DesiredRotation = Quaternion.Euler (0, -180, 0);
    } else if(Input.GetKey("d")){
        DesiredRotation = Quaternion.Euler (0, 0, 0);
    } else if(Input.GetKey("s")){
        DesiredRotation = Quaternion.Euler (0, 90, 0);
    }else {
        DesiredRotation = Quaternion.Euler (0, -90, 0);
    }
    transform.rotation = Quaternion.Lerp (transform.rotation, DesiredRotation, Time.deltaTime*RotationSpeed);
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