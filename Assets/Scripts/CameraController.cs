using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float sensX;
    public float sensY;
    public Transform orientation;
    float yRotation;
    float xRotation;
    public Transform cameraPosition;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        offset = transform.position ;
    }

    private void Update(){
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        //stop from rotating too much
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.position = cameraPosition.position;
        //player.transform.rotation.x = Quaternion.Euler(xRotation, 0);
        //orientation.rotation = Quaternion.Euler(player.transform.rotation.y, 0, yRotation);

    }

    //cam behind player
    void LateUpdate(){
        transform.position = player.transform.position + offset;
    }

}

