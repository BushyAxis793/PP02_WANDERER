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
    float inputY;
    float inputX;

    Rigidbody myRigidbody;
    CharacterController charController;
    Quaternion baseRotation;

    bool isCrouching = false;
    bool isRunning = false;
    bool isAlive = true;
    bool isGrounded = true;


    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        movementSpeed = 10f;
        transform.rotation = baseRotation;
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
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.right * inputX + transform.forward * inputZ;
        charController.Move(moveVector * movementSpeed * Time.deltaTime);
        //todo poprawic poruszanie się

    }





}
