using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementSpeed;
    float walkSpeed = 10f;
    float crouchSpeed = 5f;
    float runSpeed = 15f;
    float jumpSpeed = 5f;


    Rigidbody myRigidbody;

    bool isCrouching = false;
    bool isRunning = false;
    bool isAlive = true;
    bool isGrounded = true;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (!isCrouching)
            {
                myRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
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
        //todo poprawić na bardziej naturalny ruch
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, transform.position.z);
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddForce(movement/2);
        }
    }
}
