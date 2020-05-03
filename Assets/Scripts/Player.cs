using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementSpeed;
    float walkSpeed = 5f;
    float crouchSpeed = 2f;
    float runSpeed = 10f;
    float jumpSpeed = 5f;


    Rigidbody myRigidbody;
    CharacterController charController;
    Camera cam;
    Quaternion baseRotation;

    bool isCrouching = false;
    bool isRunning = false;
    bool isAlive = true;
    bool isGrounded = true;

    Vector3 moveDirection = Vector3.zero;


    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        movementSpeed = 10f;
    }


    void Update()
    {
        if (isAlive)
        {

            PlayerMovement();
            PlayerCrouch();
            PlayerRun();
            PlayerJump();
        }
        else
        {
            Die();
        }
        print(movementSpeed);

    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isCrouching)
            {
                //myRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                moveDirection.y = jumpSpeed * Time.deltaTime;
                //todo naprawic skok
            }
        }
    }

    private void PlayerRun()
    {
        if (!isCrouching)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                isRunning = true;
                movementSpeed = runSpeed;
            }
            else
            {
                isRunning = false;
                movementSpeed = walkSpeed;
            }
        }

    }

    private void PlayerCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                isCrouching = false;
            }
            else
            {
                isCrouching = true;
            }

            if (isCrouching)
            {
                Vector3 crouchPos = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
                Camera.main.transform.position = crouchPos;
                movementSpeed = crouchSpeed;
            }
            else
            {
                Vector3 nonCrouchPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
                Camera.main.transform.position = nonCrouchPos;
                movementSpeed = walkSpeed;
            }
        }


    }

    private void PlayerMovement()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xMouse = Input.GetAxis("Mouse X");

        moveDirection = new Vector3(0, 0, zAxis);
        moveDirection = transform.TransformDirection(moveDirection * movementSpeed);

        charController.Move(moveDirection  * Time.deltaTime);

        transform.Rotate(0, xMouse, 0);

    }





}
