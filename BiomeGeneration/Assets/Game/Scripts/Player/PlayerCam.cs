using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Mouse sensitivity")]
    public float sensX;
    public float sensY;

    [Header("Mouse rotation")]
    private float xRotation;
    private float yRotation;
    
    [Header("Orientation of player camera")]
    public Transform orientation;

    private void Start()
    {
        //Cursor settings
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Gets mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime*sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y")* Time.deltaTime*sensY;

        // Calculates Rotation
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        //Rotate cam and orientation
        transform.rotation=Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
