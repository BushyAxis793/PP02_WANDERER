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
    float gravityForce = 20f;


    Rigidbody myRigidbody;
    CharacterController charController;
    Camera cam;
    Quaternion baseRotation;

    bool isCrouching = false;
    bool isRunning = false;
    bool isAlive = true;
    bool isGrounded = true;
    bool isMoving;

    private Vector3 movingDirection = Vector3.zero;

    private float inputX, inputY;
    private float inputSetX, inputSetY;
    private float inputModFactor;

    private bool limitDiagonalSpeed = true;
    private float antiBumpFactor = 0.75f;

    [SerializeField] GameObject weaponCam;

    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        movementSpeed = 10f;
        isMoving = false;

    }


    void Update()
    {
        if (isAlive)
        {

            PlayerMovement();
            PlayerCrouch();
            PlayerRun();
        }
        else
        {
            Die();
        }
        print(movementSpeed);

    }

    private void Die()
    {
        //todo ekran game over
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!isCrouching)
            {
                movingDirection.y = jumpSpeed;

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

            CrouchPosition();
        }


    }

    private void CrouchPosition()
    {
        if (isCrouching)
        {
            Vector3 crouchPos = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
            weaponCam.transform.position = crouchPos;
            movementSpeed = crouchSpeed;
        }
        else
        {
            Vector3 nonCrouchPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            weaponCam.transform.position = nonCrouchPos;
            movementSpeed = walkSpeed;
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                inputSetY = 1f;
            }
            else
            {
                inputSetY = -1f;
            }

        }
        else
        {
            inputSetY = 0f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                inputSetX = -1f;
            }
            else
            {
                inputSetX = 1f;
            }


        }
        else
        {
            inputSetX = 0f;
        }

        inputY = Mathf.Lerp(inputY, inputSetY, Time.deltaTime * 12f);
        inputX = Mathf.Lerp(inputX, inputSetX, Time.deltaTime * 12f);


        if (isGrounded)
        {
            movingDirection = new Vector3(inputX, -antiBumpFactor,
                inputY);
            movingDirection = transform.TransformDirection(movingDirection) * movementSpeed;

            PlayerJump();

        }
        movingDirection.y -= gravityForce * Time.deltaTime;

        isGrounded = (charController.Move(movingDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

        isMoving = charController.velocity.magnitude > 0.15f;
    }





}
