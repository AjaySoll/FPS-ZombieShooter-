using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
// These variables control the movement functions for the player 
    public Camera playerCamera;
    public float walk = 4f;
    public float run = 10f;
    public float jump = 5f;
    public float gravity = 15f;

// controls the camera look speed and how fast the look speed is when the mouse input is moved  
    public float Sensitivity =4f;
    public float lookXLimit = 45f;

    // allows the innput of an animator controller 
    public Animator animator;

    //intially is used to initialise a vector that will be later used to control the movement of a game object 
    // this creates a new varaibles called movement direction as a type of vector and sets all of the vector values "x,y,z" to zero 
    Vector3 movementDirection = Vector3.zero;
    // this creates a float variable called rotationx and sets it to zero, this will later be used to control a game object 
    float rotationX = 0;

    //activates the canMove bool value
    public bool canMove = true;


    CharacterController characterController;
    void Start()
    {
        // this will allow the script to be attached to the character model and get the character model 
        characterController = GetComponent<CharacterController>();
        // this will lock the cursor to the middle of the screen 
        Cursor.lockState = CursorLockMode.Locked;
        // this will hide the cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // if character is not dead then functons will resume as normal         
        if (!Death.ISDead)
        {
            // if the game is not paused the game functions will resume 
            if (!PauseMenu.IsPaused)
            {
                 
                // Handles Movment
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);

                // this handles the sprint function and in order to activate this the left shift button has to be pressed
                bool isRunning = Input.GetKey(KeyCode.LeftShift);
                float cursorSpeedX = canMove ? (isRunning ? run : walk) * Input.GetAxis("Vertical") : 0;
                float cursorSpeedY = canMove ? (isRunning ? run : walk) * Input.GetAxis("Horizontal") : 0;
                float movementDirectionY = movementDirection.y;
                movementDirection = (forward * cursorSpeedX) + (right * cursorSpeedY);

               // this sends the float values to the animator to differentiate between whether the player is not moving (speed will be zero and idle animation will play) or if the player is moving (speed will be above 0 and therefore moving animation will play) 
                animator.SetFloat("Speed", Mathf.Abs(cursorSpeedX) + Mathf.Abs(cursorSpeedY));

               
                //Handles Jumping function and to activate spacebar has to be pressed 
                if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
                {
                    movementDirection.y = jump;
                }
                else
                {
                    movementDirection.y = movementDirectionY;
                }
                // This controls the "gravity" to ensure that when the player jumps, the player object will then return to the ground 
                if (!characterController.isGrounded)
                {
                    movementDirection.y -= gravity * Time.deltaTime;
                }

                

                 
                // Handles Player Rotation
                characterController.Move(movementDirection * Time.deltaTime);
                // this handles the sensitivity of the mouse and how fast the player screen rotates, and the player camera and the current positiion it is in 
                if (canMove)
                {                    
                    rotationX += -Input.GetAxis("Mouse Y") * Sensitivity;
                    rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                    transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Sensitivity, 0);
                }

                
            }
        }
       
    }
       
}

//reference Dustin Morman: https://www.youtube.com/watch?v=qQLvcS9FxnY